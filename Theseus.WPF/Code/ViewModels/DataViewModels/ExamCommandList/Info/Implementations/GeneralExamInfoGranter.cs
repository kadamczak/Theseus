using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations
{
    public class GeneralExamInfoGranter : InfoGranter<Exam>
    {
        private readonly ExamSetStatsStore _examSetStatsStore;
        private readonly IGetExamsOfPatientQuery _getExamsOfPatientQuery;
        private readonly DescriptiveValueComparer _valueComparer;

        public GeneralExamInfoGranter(DescriptiveValueComparer valueComparer,
                                      IGetExamsOfPatientQuery getExamsOfPatientQuery,
                                      ExamSetStatsStore examSetStatsStore)
        {
            _valueComparer = valueComparer;
            _getExamsOfPatientQuery = getExamsOfPatientQuery;
            _examSetStatsStore = examSetStatsStore;
        }

        private class ExamStats
        {
            public Guid PatientId { get; set; }
            public DateTime CreatedAt { get; set; }
            public int AttemptNumber { get; set; }
            public bool NoSkips { get; set; }
            public float TotalExamTime { get; set; }
            public int CompletedMazeAmount { get; set; }
            public int TotalInputs { get; set; }
        }

        public override string GrantInfo(CommandViewModel<Exam> commandViewModel)
        {
            Guid examSetId = commandViewModel.Model.ExamSet.Id;
            Guid groupId = commandViewModel.Model.Patient.Group.Id;

            ExamSetStatSummary examSetStatSummary = _examSetStatsStore.ExamSetStatList.Where(e => e.GroupId == groupId && e.ExamSetId == examSetId).First();
            ExamStats currentExamStats = CalculateExamStats(commandViewModel.Model);

            List<string> displayedInfoText = CreateBasicTextSummary(currentExamStats, examSetStatSummary);
            displayedInfoText.AddRange(CreateComparisonTextToOtherPatients(currentExamStats, examSetStatSummary));
            displayedInfoText.AddRange(CreateComparisonTextToPreviousAttempt(currentExamStats, examSetStatSummary));

            return string.Join("\n", displayedInfoText.ToArray());
        }

        private ExamStats CalculateExamStats(Exam exam)
        {
            var examStages = exam.Stages;
            var examSteps = exam.Stages.SelectMany(s => s.Steps);

            return new ExamStats()
            {
                PatientId = exam.Patient.Id,
                CreatedAt = exam.CreatedAt,
                AttemptNumber = CalculateAttemptNumber(exam),
                NoSkips = examStages.All(e => e.Completed),
                TotalExamTime = examSteps.Sum(e => e.TimeBeforeStep),
                CompletedMazeAmount = examStages.Where(s => s.Completed).Count(),
                TotalInputs = examSteps.Count()
            };
        }

        private int CalculateAttemptNumber(Exam exam) => _getExamsOfPatientQuery.GetExams(exam.Patient.Id)
                                                                                .Where(e => e.ExamSet.Id == exam.ExamSet.Id)
                                                                                .Where(e => e.CreatedAt < exam.CreatedAt)
                                                                                .Count() + 1;

        private List<string> CreateBasicTextSummary(ExamStats currentExamStats, ExamSetStatSummary examSetStatSummary)
        {
            return new List<string>
            {
                $"Attempt #{currentExamStats.AttemptNumber}",
                $"Completed mazes: {currentExamStats.CompletedMazeAmount}/{examSetStatSummary.MazeAmount}",
                $"Total time: {Round(currentExamStats.TotalExamTime)} s",
                $"Inputs made: {currentExamStats.TotalInputs}/{examSetStatSummary.IdealStepAmount}"
            };
        }

        private List<string> CreateComparisonTextToOtherPatients(ExamStats currentExamStats, ExamSetStatSummary examSetStatSummary)
        {
            var examsByOtherPatients = examSetStatSummary.Exams.Where(e => e.Patient.Id != currentExamStats.PatientId);

            return examsByOtherPatients.Any() ? CreateFullComparisonTextToOtherPatients(currentExamStats, CalculateAverageStats(examsByOtherPatients)) :
                                                new List<string>() { "Other patients in group didn't complete this exam set." };
        }

        public class AverageStats
        {
            public int TotalExamAmount { get; set; }
            public int ExamsWithNoSkipsAmount { get; set; }
            public float AverageCompletedMazes { get; set; }
            public float? AverageTotalTime { get; set; }
            public float? AverageTotalInputs { get; set; }
        }

        private AverageStats CalculateAverageStats(IEnumerable<Exam> exams)
        {
            var examsByOtherPatientsWithNoSkips = exams.Where(e => !e.Stages.Where(s => !s.Completed).Any());
            bool anyCompletedExamsByOtherPatient = examsByOtherPatientsWithNoSkips.Any();

            return new AverageStats()
            {
                TotalExamAmount = exams.Count(),
                ExamsWithNoSkipsAmount = examsByOtherPatientsWithNoSkips.Count(),
                AverageCompletedMazes = CalculateAverageCompletedMazes(exams),
                AverageTotalTime = anyCompletedExamsByOtherPatient ? CalculateAverageTotalTime(examsByOtherPatientsWithNoSkips) : null,
                AverageTotalInputs = anyCompletedExamsByOtherPatient ? CalculateAverageTotalInputs(examsByOtherPatientsWithNoSkips) : null
            };
        }

        private float CalculateAverageCompletedMazes(IEnumerable<Exam> exams) => (float) exams.Average(e => e.Stages.Count(s => s.Completed));
        private float CalculateAverageTotalTime(IEnumerable<Exam> exams) => exams.Average(e => e.Stages.Sum(s => s.Steps.Sum(s => s.TimeBeforeStep)));
        private float CalculateAverageTotalInputs(IEnumerable<Exam> exams) => (float) exams.Average(e => e.Stages.Sum(s => s.Steps.Count));

        private List<string> CreateFullComparisonTextToOtherPatients(ExamStats currentExamStats, AverageStats otherPatientsStats)
        {
            string completedMazesComparison = _valueComparer.Compare(currentExamStats.CompletedMazeAmount, otherPatientsStats.AverageCompletedMazes, higherIsBetter: true);
            string averageCompletedMazesFormatted = Round(otherPatientsStats.AverageCompletedMazes);

            List<string> comparisonText =  new List<string>() {
                    $"Amount of attempts by other patients: {otherPatientsStats.TotalExamAmount}",
                    $"Amount of attempts with no skips by other patients: {otherPatientsStats.ExamsWithNoSkipsAmount}",
                    "", "Comparison to average:",
                    $"\tCompleted mazes: {completedMazesComparison} (Avg: {averageCompletedMazesFormatted})"
            };

            if (currentExamStats.NoSkips && otherPatientsStats.ExamsWithNoSkipsAmount > 0)
                comparisonText.AddRange(CreateComparisonTextForTimeAndInputs(currentExamStats, otherPatientsStats.AverageTotalTime.Value, otherPatientsStats.AverageTotalInputs.Value, "Avg"));

            return comparisonText;
        }

        private string Round(float value, string suffix = "") => Math.Round(value, 1).ToString() + suffix;

        private IEnumerable<string> CreateComparisonTextToPreviousAttempt(ExamStats currentExamStats, ExamSetStatSummary examSetStatSummary)
        {
            var previousAttempt = examSetStatSummary.Exams
                                                     .Where(e => e.Patient.Id == currentExamStats.PatientId)
                                                     .Where(e => e.CreatedAt < currentExamStats.CreatedAt)
                                                     .OrderByDescending(e => e.CreatedAt)
                                                     .FirstOrDefault();

            return (previousAttempt is not null) ? CreateFullComparisonTextToPreviousAttempt(currentExamStats, CalculateExamStats(previousAttempt)) :
                                                   new List<string>() { "", "No previous attempts by this patient." };
        }

        private List<string> CreateFullComparisonTextToPreviousAttempt(ExamStats currentExamStats, ExamStats previousExamStats)
        {
            string completedMazesComparison = _valueComparer.Compare(currentExamStats.CompletedMazeAmount, previousExamStats.CompletedMazeAmount, higherIsBetter: true);
            var comparisonText = new List<string>
            {
                "", $"Comparison to previous attempt on {previousExamStats.CreatedAt.ToString("dd/MM/yyyy HH:mm")}:",
                    $"\tCompleted mazes: {completedMazesComparison} (Prev: {previousExamStats.CompletedMazeAmount})",
            };

            if (currentExamStats.NoSkips && previousExamStats.NoSkips)
                comparisonText.AddRange(CreateComparisonTextForTimeAndInputs(currentExamStats, previousExamStats.TotalExamTime, previousExamStats.TotalInputs, "Prev"));

            return comparisonText;
               
        }

        private List<string> CreateComparisonTextForTimeAndInputs(ExamStats currentExamStats, float time, float inputs, string valueType)
        {
            string timeComparison = _valueComparer.Compare(currentExamStats.TotalExamTime, time, higherIsBetter: false);
            string inputComparison = _valueComparer.Compare(currentExamStats.TotalInputs, inputs, higherIsBetter: false);
            string timeFormatted = Round(time, " s");
            string inputsFormatted = Round(inputs);

            return new List<string>
            {
                $"\tTotal time: {timeComparison} ({valueType}: {timeFormatted})",
                $"\tInputs made: {inputComparison} ({valueType}: {inputsFormatted})"
            };
        }
    }
}