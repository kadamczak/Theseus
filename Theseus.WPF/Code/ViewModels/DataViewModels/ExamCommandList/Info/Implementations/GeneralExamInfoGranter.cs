using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations
{
    public class GeneralExamInfoGranter : InfoGranter<Exam>
    {
        private readonly ExamSetStatsStore _examSetStatsStore;
        private readonly DescriptiveValueComparer _valueComparer;

        public GeneralExamInfoGranter(DescriptiveValueComparer valueComparer, ExamSetStatsStore examSetStatsStore)
        {
            _valueComparer = valueComparer;
            _examSetStatsStore = examSetStatsStore;
        }

        private class ExamStats
        {
            public Guid PatientId { get; set; }
            public DateTime CreatedAt { get; set; }
            public float TotalExamTime { get; set; }
            public int CompletedMazeAmount { get; set; }
            public int TotalSteps { get; set; }
        }

        public override string GrantInfo(CommandViewModel<Exam> commandViewModel)
        {
            Guid examId = commandViewModel.Model.Id;
            Guid examSetId = commandViewModel.Model.ExamSet.Id;
            Guid groupId = commandViewModel.Model.Patient.Group.Id;

            var examStages = commandViewModel.Model.Stages;
            var examSteps = commandViewModel.Model.Stages.SelectMany(s => s.Steps);

            ExamSetStatSummary examSetStatSummary = _examSetStatsStore.ExamSetStatList.Where(e => e.GroupId == groupId && e.ExamSetId == examSetId).First();

            ExamStats currentExamStats = new ExamStats()
            {
                PatientId = commandViewModel.Model.Patient.Id,
                CreatedAt = commandViewModel.Model.CreatedAt,
                TotalExamTime = examSteps.Sum(e => e.TimeBeforeStep),
                CompletedMazeAmount = examStages.Where(s => s.Completed).Count(),
                TotalSteps = examSteps.Count()
            };

            List<string> displayedInfoText = CreateBasicSummary(currentExamStats, examSetStatSummary);
            displayedInfoText.AddRange(CreateComparisonTextWithOverallSummary(currentExamStats, examSetStatSummary));
            displayedInfoText.AddRange(CreateComparisonWithPreviousAttempt(currentExamStats, examSetStatSummary));

            return string.Join("\n", displayedInfoText.ToArray());
        }

        private List<string> CreateBasicSummary(ExamStats currentExamStats, ExamSetStatSummary examSetStatSummary)
        {
            return new List<string>
            {
                $"Completed mazes: {currentExamStats.CompletedMazeAmount}/{examSetStatSummary.MazeAmount}",
                $"Total time: {Math.Round(currentExamStats.TotalExamTime, 2)} s",
                $"Inputs made: {currentExamStats.TotalSteps}/{examSetStatSummary.IdealStepAmount}"
            };
        }

        private List<string> CreateComparisonTextWithOverallSummary(ExamStats currentExamStats, ExamSetStatSummary examSetStatSummary)
        {
            var examsByOtherPatients = examSetStatSummary.Exams.Where(e => e.Patient.Id != currentExamStats.PatientId);

            if(examsByOtherPatients.Any())
            {
                var examsByOtherPatientsWithNoSkips = examsByOtherPatients.Where(e => !e.Stages.Where(s => !s.Completed).Any());

                List<string> comparisonInfo = new List<string>() {
                    $"Amount of attempts by other patients: {examsByOtherPatients.Count()}",
                    $"Amount of attempts with no skips by other patients: {examsByOtherPatientsWithNoSkips.Count()}",
                    "", "Comparison to average:"
                };

                float averageCompletedMazes = (float) examsByOtherPatients.Average(e => e.Stages.Count(s => s.Completed));
                bool anyCompletedExamsByOtherPatient = examsByOtherPatientsWithNoSkips.Any();

                float? averageTotalTime = anyCompletedExamsByOtherPatient ? (float)examsByOtherPatientsWithNoSkips.Average(e => e.Stages.Sum(s => s.Steps.Sum(s => s.TimeBeforeStep))) : null;
                float? averageTotalInputs = anyCompletedExamsByOtherPatient ? (float)examsByOtherPatientsWithNoSkips.Average(e => e.Stages.Sum(s => s.Steps.Count)) : null;

                comparisonInfo.Add("\tCompleted mazes: " + _valueComparer.Compare(currentExamStats.CompletedMazeAmount, averageCompletedMazes, higherIsBetter: true));
                comparisonInfo.Add("\tTotal time: " + _valueComparer.Compare(currentExamStats.TotalExamTime, averageTotalTime, higherIsBetter: false));
                comparisonInfo.Add("\tInputs made: " + _valueComparer.Compare(currentExamStats.TotalSteps, averageTotalInputs, higherIsBetter: false));

                return comparisonInfo;
            }
            else
            {
                return new List<string>() { "Other patients in group didn't complete this exam set."};
            }
        }

        private IEnumerable<string> CreateComparisonWithPreviousAttempt(ExamStats currentExamStats, ExamSetStatSummary examSetStatSummary)
        {
            var previousAttempt = examSetStatSummary.Exams
                                                     .Where(e => e.Patient.Id == currentExamStats.PatientId)
                                                     .Where(e => e.CreatedAt < currentExamStats.CreatedAt)
                                                     .OrderByDescending(e => e.CreatedAt)
                                                     .FirstOrDefault();
            if (previousAttempt is null)
            {
                return new List<string>() {"", "No previous attempts by this patient." };
            }
            else
            {
                int previousCompletedMazes = previousAttempt.Stages.Count(s => s.Completed);
                bool allMazesCompleted = previousAttempt.Stages.All(s => s.Completed);

                float? previousTotalTime = (allMazesCompleted) ? previousAttempt.Stages.Sum(s => s.Steps.Sum(s => s.TimeBeforeStep)) : null;
                float? previousTotalInputs = (allMazesCompleted) ? previousAttempt.Stages.Sum(s => s.Steps.Count) : null;

                return new List<string>() {
                    "", $"Comparison to previous attempt on {previousAttempt.CreatedAt.ToString("dd/MM/yyyy HH:mm")}:",
                    "\tCompleted mazes: " + _valueComparer.Compare(currentExamStats.CompletedMazeAmount, previousCompletedMazes, higherIsBetter: true),
                    "\tTotal time: " + _valueComparer.Compare(currentExamStats.TotalExamTime, previousTotalTime, higherIsBetter: false),
                    "\tInputs made: " + _valueComparer.Compare(currentExamStats.TotalSteps, previousTotalInputs, higherIsBetter: false)
                };
            }
        }
    }
}