using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.WPF.Code.Stores.Exams;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations
{
    public class GeneralExamInfoGranter : InfoGranter<Exam>
    {
        private readonly IGetStagesOfExamQuery _getStagesQuery;
        private readonly IGetStepsOfExamQuery _getStepsQuery;
        private readonly ExamSetStatsStore _examSetStatsStore;

        public GeneralExamInfoGranter(IGetStagesOfExamQuery getStagesQuery,
                                      IGetStepsOfExamQuery getStepsQuery,
                                      ExamSetStatsStore examSetStatsStore)
        {
            _getStagesQuery = getStagesQuery;
            _getStepsQuery = getStepsQuery;
            _examSetStatsStore = examSetStatsStore;
        }

        public class ExamStats
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

            var examStages = _getStagesQuery.GetStages(examId);
            var examSteps = _getStepsQuery.GetSteps(examId);

            ExamSetStatSummary examSetStatSummary = _examSetStatsStore.ExamSetStatList.Where(e => e.GroupId == groupId && e.ExamSetId == examSetId).First();

            ExamStats currentExamStats = new ExamStats()
            {
                PatientId = commandViewModel.Model.Patient.Id,
                CreatedAt = commandViewModel.Model.CreatedAt,
                TotalExamTime = examSteps.Sum(s => s.TimeBeforeStep),
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
                    "",
                    "Comparison to average:"
                };

                float averageCompletedMazes = (float) examsByOtherPatients.Average(e => e.Stages.Count(s => s.Completed));
                bool anyCompletedExamsByOtherPatient = examsByOtherPatientsWithNoSkips.Any();

                float? averageTotalTime = anyCompletedExamsByOtherPatient ?
                                          (float)examsByOtherPatientsWithNoSkips.Average(e => e.Stages.Sum(s => s.Steps.Sum(s => s.TimeBeforeStep))) : null;
                
                float? averageTotalInputs = anyCompletedExamsByOtherPatient ?
                                          (float)examsByOtherPatientsWithNoSkips.Average(e => e.Stages.Sum(s => s.Steps.Count)) : null;

                comparisonInfo.Add("Completed mazes: " + CreateValueComparison(currentExamStats.CompletedMazeAmount, averageCompletedMazes));
                comparisonInfo.Add("Total time: " + CreateValueComparison(currentExamStats.TotalExamTime, averageTotalTime));
                comparisonInfo.Add("Inputs made: " + CreateValueComparison(currentExamStats.TotalSteps, averageTotalInputs));

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
                return new List<string>() { "", "No previous attempts by this patient." };
            }
            else
            {
                int previousCompletedMazes = previousAttempt.Stages.Count(s => s.Completed);
                bool allMazesCompleted = previousAttempt.Stages.All(s => s.Completed);

                float? previousTotalTime = (allMazesCompleted) ? previousAttempt.Stages.Sum(s => s.Steps.Sum(s => s.TimeBeforeStep)) : null;
                float? previousTotalInputs = (allMazesCompleted) ? previousAttempt.Stages.Sum(s => s.Steps.Count) : null;

                return new List<string>() {
                    "",
                    $"Comparison to previous attempt on {previousAttempt.CreatedAt.ToString("dd/MM/yyyy HH:mm")}:",
                    "Completed mazes: " + CreateValueComparison(currentExamStats.CompletedMazeAmount, previousCompletedMazes),
                    "Total time: " + CreateValueComparison(currentExamStats.TotalExamTime, previousTotalTime),
                    "Total time: " + CreateValueComparison(currentExamStats.TotalExamTime, previousTotalInputs)
                };
            }
        }

        private string CreateValueComparison(float examValue, float? possibleValue)
        {
            if (possibleValue is null)
                return "No other values to compare.";

            float value = possibleValue.Value;

            if (examValue == value)
            {
                return "Same as.";
            }
            else if (examValue > value)
            {
                float percentageHigher = (examValue - value) / value * 100f;
                return $"Higher by {Math.Round(percentageHigher, 1)}%.";
            }
            else
            {
                float percentageLower = (value - examValue) / examValue * 100f;
                return $"Lower by {Math.Round(percentageLower, 1)}%.";
            }
        }
    }
}