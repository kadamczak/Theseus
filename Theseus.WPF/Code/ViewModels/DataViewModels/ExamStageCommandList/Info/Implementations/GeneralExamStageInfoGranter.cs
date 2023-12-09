using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;
using static Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info.Implementations.GeneralExamStageInfoGranter;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info.Implementations
{
    public class GeneralExamStageInfoGranter : InfoGranter<ExamStageWithMazeViewModel>
    {
        private readonly DescriptiveValueComparer _valueComparer;
        private readonly IGetExamStagesOfExamSetOfIndexQuery _getExamStagesQuery;
        private readonly IGetMazeOfExamStageQuery _getMazeOfExamStageQuery;

        public GeneralExamStageInfoGranter(DescriptiveValueComparer descriptiveValueComparer,
                                           IGetExamStagesOfExamSetOfIndexQuery getExamStagesQuery,
                                           IGetMazeOfExamStageQuery getMazeQuery)
        {
            _valueComparer = descriptiveValueComparer;
            _getExamStagesQuery = getExamStagesQuery;
            _getMazeOfExamStageQuery = getMazeQuery;
        }

        public class ExamStageStats
        {
            public Guid PatientId { get; set; }
            public Guid ExamStageId { get; set; }
            public Guid ExamSetId { get; set; }
            public int Index { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool Completed { get; set; }
            public ExamStageData Data { get; set; }
        }

        public class ExamStageData
        {
            public float TotalInputs { get; set; }
            public float? TotalTime { get; set; }
            public float? TimeBeforeFirstInput { get; set; }
            public float? LongestInactivityTime { get; set; }
        }

        public override string GrantInfo(CommandViewModel<ExamStageWithMazeViewModel> commandViewModel)
        {
            ExamStageStats currentExamStageStats = CalculateExamStageStats(commandViewModel.Model.ExamStage);

            List<string> displayedInfoText = CreateBasicSummary(currentExamStageStats);
            displayedInfoText.AddRange(CreateComparisonTextToOtherPatients(currentExamStageStats));
            displayedInfoText.AddRange(CreateComparisonTextToPreviousAttempt(currentExamStageStats));

            return string.Join("\n", displayedInfoText.ToArray());
        }

        private ExamStageStats CalculateExamStageStats(ExamStage examStage)
        {
            Exam exam = examStage.Exam;

            return new ExamStageStats()
            {
                PatientId = exam.Patient.Id,
                ExamStageId = examStage.Id,
                ExamSetId = exam.ExamSet.Id,
                Index = examStage.Index,
                CreatedAt = exam.CreatedAt,
                Completed = examStage.Completed,

                Data = new ExamStageData()
                {
                    TotalInputs = examStage.Steps.Count,
                    TotalTime = examStage.Steps.Any() ? examStage.Steps.Sum(s => s.TimeBeforeStep) : null,
                    TimeBeforeFirstInput = examStage.Steps.Any() ? examStage.Steps.First().TimeBeforeStep : null,
                    LongestInactivityTime = examStage.Steps.Any() ? examStage.Steps.Max(s => s.TimeBeforeStep) : null
                }
            };
        }

        private List<string> CreateBasicSummary(ExamStageStats stats)
        {
            var maze = _getMazeOfExamStageQuery.GetMaze(stats.ExamStageId);
            int idealInputAmount = maze.SolutionPath.Count;

            List<string> textSummary = new List<string>
            {
                "Completed: " + (stats.Completed ? "Yes" : "No"),
                $"Inputs made: {Round(stats.Data.TotalInputs)}/{idealInputAmount}",
            };

            if (stats.Data.TotalInputs > 0)
            {
                textSummary.AddRange(new List<string>
                {
                    $"Total time: {Round(stats.Data.TotalTime!.Value)} s",
                    $"Time before first input: {Round(stats.Data.TimeBeforeFirstInput!.Value)} s",
                    $"Longest inactivity time: {Round(stats.Data.LongestInactivityTime!.Value)} s",
                });
            }
            else
            {
                textSummary.Add("Times not recorded (inputs were not made).");
            }
            return textSummary;
        }

        private IEnumerable<string> CreateComparisonTextToOtherPatients(ExamStageStats currentExamStageStats)
        {
            var examStagesOfOtherPatients = _getExamStagesQuery.GetExamStages(currentExamStageStats.ExamSetId, currentExamStageStats.Index)
                                                               .Where(e => e.Exam.Patient.Id != currentExamStageStats.PatientId);

            return examStagesOfOtherPatients.Any() ? CreateFullComparisonTextToOtherPatients(currentExamStageStats, CalculateAverageStats(examStagesOfOtherPatients)) :
                                                     new List<string>() { "Other patients in group didn't complete this maze." };
        }

        public class AverageExamStageStats
        {
            public int TotalAttemptAmount { get; set; }
            public int CompletedAttemptAmount { get; set; }
            public ExamStageData Data { get; set; }
        }

        private AverageExamStageStats CalculateAverageStats(IEnumerable<ExamStage> examStages)
        {
            var completedExamStages = examStages.Where(e => e.Completed);

            return new AverageExamStageStats()
            {
                TotalAttemptAmount = examStages.Count(),
                CompletedAttemptAmount = completedExamStages.Count(),
                Data = new ExamStageData()
                {
                    TotalTime = completedExamStages.Any() ? CalculateAverageTotalTime(completedExamStages) : null,
                    TotalInputs = completedExamStages.Any() ? CalculateAverageTotalInputs(completedExamStages) : 0,
                    TimeBeforeFirstInput = completedExamStages.Any() ? CalculateAverageTimeBeforeFirstInput(completedExamStages) : null,
                    LongestInactivityTime = completedExamStages.Any() ? CalculateAverageLongestInactivityTime(completedExamStages) : null
                }
            };
        }

        private float CalculateAverageTotalTime(IEnumerable<ExamStage> examStages) => (float) examStages.Average(e => e.Steps.Sum(e => e.TimeBeforeStep));
        private float CalculateAverageTotalInputs(IEnumerable<ExamStage> examStages) => (float) examStages.Average(e => e.Steps.Count);
        private float CalculateAverageTimeBeforeFirstInput(IEnumerable<ExamStage> examStages) => (float) examStages.Average(e => e.Steps.First().TimeBeforeStep);
        private float CalculateAverageLongestInactivityTime(IEnumerable<ExamStage> examStages) => (float) examStages.Average(e => e.Steps.Max(e => e.TimeBeforeStep));

        private List<string> CreateFullComparisonTextToOtherPatients(ExamStageStats examStageStats, AverageExamStageStats averageExamStageStats)
        {
            string formattedPercentCompleted = Round((float) averageExamStageStats.CompletedAttemptAmount / averageExamStageStats.TotalAttemptAmount * 100f);

            List<string> comparisonText = new List<string>() {
                    "",
                    $"Amount of attempts by other patients: {averageExamStageStats.TotalAttemptAmount}",
                    $"Amount of completed attempts by other patients: {averageExamStageStats.CompletedAttemptAmount} ({formattedPercentCompleted}%)"
            };

            if (examStageStats.Completed && averageExamStageStats.CompletedAttemptAmount > 0)
            {
                comparisonText.AddRange(new List<string>() { "Comparison to average:" });
                comparisonText.AddRange(CreateComparisonOfCompletedStages(examStageStats, averageExamStageStats.Data, "Avg"));
            }

            return comparisonText;
        }

        private IEnumerable<string> CreateComparisonOfCompletedStages(ExamStageStats examStageStats, ExamStageData otherExamStageData, string valueType)
        {
            string timeComparison = _valueComparer.Compare(examStageStats.Data.TotalTime!.Value, otherExamStageData.TotalTime, higherIsBetter: false);
            string inputComparison = _valueComparer.Compare(examStageStats.Data.TotalInputs, otherExamStageData.TotalInputs, higherIsBetter: false);
            string firstInputTimeComparison = _valueComparer.Compare(examStageStats.Data.TimeBeforeFirstInput!.Value, otherExamStageData.TimeBeforeFirstInput!.Value, "Higher", "Lower");
            string longestInactivityTimeComparison = _valueComparer.Compare(examStageStats.Data.LongestInactivityTime!.Value, otherExamStageData.LongestInactivityTime!.Value, "Higher", "Lower");

            string timeFormatted = Round(otherExamStageData.TotalTime!.Value);
            string inputsFormatted = Round(otherExamStageData.TotalInputs);
            string firstInputTimeFormatted = Round(otherExamStageData.TimeBeforeFirstInput!.Value);
            string longestInactivityTimeFormatted = Round(otherExamStageData.LongestInactivityTime!.Value);

            return new List<string>
            {
                $"\tInputs made: {inputComparison} ({valueType}: {inputsFormatted})",
                $"\tTotal time: {timeComparison} ({valueType}: {timeFormatted} s)",
                $"\tTime before first input: {firstInputTimeComparison} ({valueType}: {firstInputTimeFormatted} s)",
                $"\tLongest inactivity time: {longestInactivityTimeComparison} ({valueType}: {longestInactivityTimeFormatted} s)"
            };
        }

        private IEnumerable<string> CreateComparisonTextToPreviousAttempt(ExamStageStats currentExamStageStats)
        {
            var previousAttempt = _getExamStagesQuery.GetExamStages(currentExamStageStats.ExamSetId, currentExamStageStats.Index)
                                                     .Where(e => e.Exam.Patient.Id == currentExamStageStats.PatientId)
                                                     .Where(e => e.Exam.CreatedAt < currentExamStageStats.CreatedAt)
                                                     .OrderByDescending(e => e.Exam.CreatedAt)
                                                     .FirstOrDefault();

            return (previousAttempt is not null) ? CreateFullComparisonTextToPreviousAttempt(currentExamStageStats, CalculateExamStageStats(previousAttempt)) :
                                                   new List<string>() { "", "No previous attempts by this patient." };
        }

        private List<string> CreateFullComparisonTextToPreviousAttempt(ExamStageStats currentExamStageStats, ExamStageStats previousExamStageStats)
        {
            var comparisonText = new List<string>
            {
                "", $"Comparison to previous attempt on {previousExamStageStats.CreatedAt.ToString("dd/MM/yyyy HH:mm")}:"
            };

            if (currentExamStageStats.Completed && previousExamStageStats.Completed)
            {
                comparisonText.AddRange(CreateComparisonOfCompletedStages(currentExamStageStats, previousExamStageStats.Data, "Prev"));
            }
            else if (currentExamStageStats.Completed)
            {
                comparisonText.Add("Previous attempt was not completed, but this one was.");
            }
            else if (previousExamStageStats.Completed)
            {
                comparisonText.Add("Previous attempt was completed, but this one was not.");
            }
            else
            {
                comparisonText.Add("Both attempts were not completed.");
            }

            return comparisonText;
        }

        private string Round(float value) => Math.Round(value, 1).ToString();
    }
}