using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info.Implementations
{
    public class GeneralExamStageInfoGranter : InfoGranter<ExamStageWithMazeViewModel>
    {
        private readonly DescriptiveValueComparer _valueComparer;
        private readonly ExamSetStatCalculator _statCalculator;
        private readonly IGetExamStagesOfExamSetOfIndexQuery _getExamStagesQuery;
        private readonly IGetMazeOfExamStageQuery _getMazeOfExamStageQuery;

        public GeneralExamStageInfoGranter(DescriptiveValueComparer descriptiveValueComparer,
                                           IGetExamStagesOfExamSetOfIndexQuery getExamStagesQuery,
                                           ExamSetStatCalculator statCalculator,
                                           IGetMazeOfExamStageQuery getMazeQuery)
        {
            _valueComparer = descriptiveValueComparer;
            _getExamStagesQuery = getExamStagesQuery;
            _statCalculator = statCalculator;
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
                "Completed:".Resource() + (stats.Completed ? "Yes".Resource() : "No".Resource()),
                $"{"InputsMade:".Resource()}{Round(stats.Data.TotalInputs)}/{idealInputAmount}",
            };

            if (stats.Data.TotalInputs > 0)
            {
                textSummary.AddRange(new List<string>
                {
                    $"{"TotalTime:".Resource()}{Round(stats.Data.TotalTime!.Value)} s",
                    $"{"TimeBeforeFirstInput:".Resource()}{Round(stats.Data.TimeBeforeFirstInput!.Value)} s",
                    $"{"LongestInactivityTime:".Resource()}{Round(stats.Data.LongestInactivityTime!.Value)} s",
                });

                if(stats.Completed)
                {
                    double score = _statCalculator.CalculateScoreForExamStage(idealInputAmount, Convert.ToInt32(stats.Data.TotalInputs), stats.Data.TotalTime!.Value);

                    textSummary.AddRange(new List<string>
                    {
                        $"{"Score".Resource()}: {Math.Round(score, 2)}/100"
                    });
                }
            }
            else
            {
                textSummary.Add("TimesNotRecorded".Resource());
            }
            return textSummary;
        }

        private IEnumerable<string> CreateComparisonTextToOtherPatients(ExamStageStats currentExamStageStats)
        {
            var examStagesOfOtherPatients = _getExamStagesQuery.GetExamStages(currentExamStageStats.ExamSetId, currentExamStageStats.Index)
                                                               .Where(e => e.Exam.Patient.Id != currentExamStageStats.PatientId);

            return examStagesOfOtherPatients.Any() ? CreateFullComparisonTextToOtherPatients(currentExamStageStats, CalculateAverageStats(examStagesOfOtherPatients)) :
                                                     new List<string>() { "OtherPatientsInGroupDidNotCompleteThisMaze".Resource() };
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
                    $"{"AmountOfAttemptsByOtherPatients:".Resource()}{averageExamStageStats.TotalAttemptAmount}",
                    $"{"AmountOfCompletedAttemptsByOtherPatients:".Resource()}{averageExamStageStats.CompletedAttemptAmount} ({formattedPercentCompleted}%)"
            };

            if (examStageStats.Completed && averageExamStageStats.CompletedAttemptAmount > 0)
            {
                comparisonText.AddRange(new List<string>() { "ComparisonToAverage:".Resource() });
                comparisonText.AddRange(CreateComparisonOfCompletedStages(examStageStats, averageExamStageStats.Data, "Avg".Resource()));
            }

            return comparisonText;
        }

        private IEnumerable<string> CreateComparisonOfCompletedStages(ExamStageStats examStageStats, ExamStageData otherExamStageData, string valueType)
        {
            string timeComparison = _valueComparer.Compare(examStageStats.Data.TotalTime!.Value, otherExamStageData.TotalTime, higherIsBetter: false);
            string inputComparison = _valueComparer.Compare(examStageStats.Data.TotalInputs, otherExamStageData.TotalInputs, higherIsBetter: false);
            string firstInputTimeComparison = _valueComparer.Compare(examStageStats.Data.TimeBeforeFirstInput!.Value, otherExamStageData.TimeBeforeFirstInput!.Value, "Higher".Resource(), "Lower".Resource());
            string longestInactivityTimeComparison = _valueComparer.Compare(examStageStats.Data.LongestInactivityTime!.Value, otherExamStageData.LongestInactivityTime!.Value, "Higher".Resource(), "Lower".Resource());

            string timeFormatted = Round(otherExamStageData.TotalTime!.Value);
            string inputsFormatted = Round(otherExamStageData.TotalInputs);
            string firstInputTimeFormatted = Round(otherExamStageData.TimeBeforeFirstInput!.Value);
            string longestInactivityTimeFormatted = Round(otherExamStageData.LongestInactivityTime!.Value);

            return new List<string>
            {
                $"\t{"InputsMade:".Resource()}{inputComparison} ({valueType}: {inputsFormatted})",
                $"\t{"TotalTime:".Resource()}{timeComparison} ({valueType}: {timeFormatted} s)",
                $"\t{"TimeBeforeFirstInput:".Resource()}{firstInputTimeComparison} ({valueType}: {firstInputTimeFormatted} s)",
                $"\t{"LongestInactivityTime:".Resource()}{longestInactivityTimeComparison} ({valueType}: {longestInactivityTimeFormatted} s)"
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
                                                   new List<string>() { "", "NoPreviousAttemptsByThisPatient".Resource() };
        }

        private List<string> CreateFullComparisonTextToPreviousAttempt(ExamStageStats currentExamStageStats, ExamStageStats previousExamStageStats)
        {
            var comparisonText = new List<string>
            {
                "", $"{"ComparisonToPreviousAttemptOn:".Resource()}{previousExamStageStats.CreatedAt.ToString("dd/MM/yyyy HH:mm")}:"
            };

            if (currentExamStageStats.Completed && previousExamStageStats.Completed)
            {
                comparisonText.AddRange(CreateComparisonOfCompletedStages(currentExamStageStats, previousExamStageStats.Data, "Prev".Resource()));
            }
            else if (currentExamStageStats.Completed)
            {
                comparisonText.Add("PreviousAttemptWasNotCompletedButThisOneWas".Resource());
            }
            else if (previousExamStageStats.Completed)
            {
                comparisonText.Add("PreviousAttemptWasCompletedButThisOneWasNot".Resource());
            }
            else
            {
                comparisonText.Add("BothAttemptsWereNotCompleted".Resource());
            }

            return comparisonText;
        }

        private string Round(float value) => Math.Round(value, 1).ToString();
    }
}