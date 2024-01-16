using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Domain.Services.ExamDataServices.Summary;
using Theseus.Domain.Services.ExamDataServices.Summary.ExamStageStats;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info.Implementations
{
    public class GeneralExamStageInfoGranter : InfoGranter<ExamStageWithMazeViewModel>
    {
        private readonly DescriptiveValueComparer _valueComparer;
        private readonly IGetExamStagesOfExamSetOfIndexWithFullIncludeQuery _getExamStagesQuery;
        private readonly IGetMazeOfExamStageQuery _getMazeOfExamStageQuery;
        private readonly ScoreCalculator _scoreCalculator;
        private readonly ExamStageCalculator _examStageCalculator;

        public GeneralExamStageInfoGranter(DescriptiveValueComparer descriptiveValueComparer,
                                           IGetExamStagesOfExamSetOfIndexWithFullIncludeQuery getExamStagesQuery,
                                           ScoreCalculator scoreCalculator,
                                           ExamStageCalculator examStageCalculator,
                                           IGetMazeOfExamStageQuery getMazeQuery)
        {
            _valueComparer = descriptiveValueComparer;
            _getExamStagesQuery = getExamStagesQuery;
            _getMazeOfExamStageQuery = getMazeQuery;
            _scoreCalculator = scoreCalculator;
            _examStageCalculator = examStageCalculator;
        }

        public override string GrantInfo(CommandViewModel<ExamStageWithMazeViewModel> commandViewModel)
        {
            ExamStageStats currentExamStageStats = _examStageCalculator.CalculateExamStageStats(commandViewModel.Model.ExamStage);

            List<string> displayedInfoText = CreateBasicSummary(currentExamStageStats);
            displayedInfoText.AddRange(CreateComparisonTextToOtherPatients(currentExamStageStats));
            displayedInfoText.AddRange(CreateComparisonTextToPreviousAttempt(currentExamStageStats));

            return string.Join("\n", displayedInfoText.ToArray());
        }
      

        private List<string> CreateBasicSummary(ExamStageStats stats)
        {
            var maze = _getMazeOfExamStageQuery.GetMaze(stats.ExamStageId);
            int idealInputAmount = maze.SolutionPath.Count;

            List<string> textSummary = new List<string>
            {
                "Completed:".Resource() + (stats.Completed ? "Yes".Resource() : "No".Resource()),
                $"{"InputsMade:".Resource()}{Round(stats.TotalInputs)}/{idealInputAmount}",
                $"{"TotalTime:".Resource()}{Round(stats.TotalTime, 2)} s",
                $"{"RedundantInputs:".Resource()}{Round(stats.RedundantInputs)}",
                $"{"WallHits:".Resource()}{Round(stats.WallHits)}",
            };

            if (stats.TotalInputs > 0)
            {
                textSummary.AddRange(new List<string>
                {
                    $"{"TimeBeforeFirstInput:".Resource()}{Round(stats.TimeBeforeFirstInput!.Value, 3)} s",
                    $"{"LongestInactivityTime:".Resource()}{Round(stats.LongestInactivityTime!.Value, 3)} s",
                });

                if(stats.Completed)
                {
                    double score = _scoreCalculator.CalculateScoreForExamStage(idealInputAmount,
                                                                              Convert.ToInt32(stats.RedundantInputs),
                                                                              Convert.ToInt32(stats.WallHits),
                                                                              stats.TotalTime);

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
            var examStagesOfOtherPatients = _getExamStagesQuery.GetExamStages(currentExamStageStats.ExamSetId, currentExamStageStats.Index, currentExamStageStats.GroupId)
                                                               .Where(e => e.Exam.Patient.Id != currentExamStageStats.PatientId);

            return examStagesOfOtherPatients.Any() ? CreateFullComparisonTextToOtherPatients(currentExamStageStats, _examStageCalculator.CalculateAverageStats(examStagesOfOtherPatients)) :
                                                     new List<string>() { "OtherPatientsInGroupDidNotCompleteThisMaze".Resource() };
        }

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
                comparisonText.AddRange(CreateComparisonOfCompletedStages(examStageStats, averageExamStageStats, "Avg".Resource()));
            }

            return comparisonText;
        }

        private class StageData
        {
            public float TotalInputs { get; set; }
            public float TotalTime { get; set; }
            public float TimeBeforeFirstInput { get; set; }
            public float LongestInactivityTime { get; set; }
            public float RedundantInputs { get; set; }
            public float WallHits { get; set; }
        }

        private StageData ConvertToStageData(ExamStageStats stats)
        {
            return new StageData()
            {
                TotalInputs = stats.TotalInputs,
                TotalTime = stats.TotalTime,
                TimeBeforeFirstInput = stats.TimeBeforeFirstInput.Value,
                LongestInactivityTime = stats.LongestInactivityTime.Value,
                RedundantInputs = stats.RedundantInputs,
                WallHits = stats.WallHits
            };
        }

        private StageData ConvertToStageData(AverageExamStageStats stats)
        {
            return new StageData()
            {
                TotalInputs = stats.TotalInputs,
                TotalTime = stats.TotalTime.Value,
                TimeBeforeFirstInput = stats.TimeBeforeFirstInput.Value,
                LongestInactivityTime = stats.LongestInactivityTime.Value,
                RedundantInputs = stats.RedundantInputs.Value,
                WallHits = stats.WallHits.Value
            };
        }

        private IEnumerable<string> CreateComparisonOfCompletedStages(ExamStageStats examStageStats, ExamStageStats otherExamStageData, string valueType)
        {
            StageData data = ConvertToStageData(otherExamStageData);
            return CreateComparisonOfCompletedStages(examStageStats, data, valueType);
        }

        private IEnumerable<string> CreateComparisonOfCompletedStages(ExamStageStats examStageStats, AverageExamStageStats otherExamStageData, string valueType)
        {
            StageData data = ConvertToStageData(otherExamStageData);
            return CreateComparisonOfCompletedStages(examStageStats, data, valueType);
        }


        private IEnumerable<string> CreateComparisonOfCompletedStages(ExamStageStats examStageStats, StageData otherExamStageData, string valueType)
        {
            string timeComparison = _valueComparer.Compare(examStageStats.TotalTime, otherExamStageData.TotalTime, higherIsBetter: false);
            string inputComparison = _valueComparer.Compare(examStageStats.TotalInputs, otherExamStageData.TotalInputs, higherIsBetter: false);
            string firstInputTimeComparison = _valueComparer.Compare(examStageStats.TimeBeforeFirstInput!.Value, otherExamStageData.TimeBeforeFirstInput, "Higher".Resource(), "Lower".Resource());
            string longestInactivityTimeComparison = _valueComparer.Compare(examStageStats.LongestInactivityTime!.Value, otherExamStageData.LongestInactivityTime, "Higher".Resource(), "Lower".Resource());
            string redundantInputsComparison = _valueComparer.Compare(examStageStats.RedundantInputs, otherExamStageData.RedundantInputs, higherIsBetter: false);
            string wallHitsComparison = _valueComparer.Compare(examStageStats.WallHits, otherExamStageData.WallHits, higherIsBetter: false);

            string timeFormatted = Round(otherExamStageData.TotalTime, 2);
            string inputsFormatted = Round(otherExamStageData.TotalInputs);
            string firstInputTimeFormatted = Round(otherExamStageData.TimeBeforeFirstInput, 3);
            string longestInactivityTimeFormatted = Round(otherExamStageData.LongestInactivityTime, 3);
            string redundantInputsFormatted = Round(otherExamStageData.RedundantInputs);
            string wallHitsFormatted = Round(otherExamStageData.WallHits);

            return new List<string>
            {
                $"\t{"InputsMade:".Resource()}{inputComparison} ({valueType}: {inputsFormatted})",
                $"\t{"TotalTime:".Resource()}{timeComparison} ({valueType}: {timeFormatted} s)",
                $"\t{"TimeBeforeFirstInput:".Resource()}{firstInputTimeComparison} ({valueType}: {firstInputTimeFormatted} s)",
                $"\t{"LongestInactivityTime:".Resource()}{longestInactivityTimeComparison} ({valueType}: {longestInactivityTimeFormatted} s)",
                $"\t{"RedundantInputs:".Resource()}{redundantInputsComparison} ({valueType}: {redundantInputsFormatted})",
                $"\t{"WallHits:".Resource()}{wallHitsComparison} ({valueType}: {wallHitsFormatted})",
            };
        }

        private IEnumerable<string> CreateComparisonTextToPreviousAttempt(ExamStageStats currentExamStageStats)
        {
            var previousAttempt = _getExamStagesQuery.GetExamStages(currentExamStageStats.ExamSetId, currentExamStageStats.Index, currentExamStageStats.GroupId)
                                                     .Where(e => e.Exam.Patient.Id == currentExamStageStats.PatientId)
                                                     .Where(e => e.Exam.CreatedAt < currentExamStageStats.CreatedAt)
                                                     .OrderByDescending(e => e.Exam.CreatedAt)
                                                     .FirstOrDefault();

            return (previousAttempt is not null) ? CreateFullComparisonTextToPreviousAttempt(currentExamStageStats, _examStageCalculator.CalculateExamStageStats(previousAttempt)) :
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
                comparisonText.AddRange(CreateComparisonOfCompletedStages(currentExamStageStats, previousExamStageStats, "Prev".Resource()));
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

        private string Round(float value, int decimalPlaces = 1) => Math.Round(value, decimalPlaces).ToString();
    }
}