using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;

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
            public int TotalInputs { get; set; }
            public float? TotalTime { get; set; }
            public float? TimeBeforeFirstInput { get; set; }
            public float? LongestInactivityTime { get; set; }
        }

        public override string GrantInfo(CommandViewModel<ExamStageWithMazeViewModel> commandViewModel)
        {
            ExamStage examStage = commandViewModel.Model.ExamStage;
            Exam exam = examStage.Exam;

            ExamStageStats currentExamStageStats = new ExamStageStats()
            {
                PatientId = exam.Patient.Id,
                ExamStageId = examStage.Id,
                ExamSetId = exam.ExamSet.Id,
                Index = examStage.Index,
                CreatedAt = exam.CreatedAt,
                Completed = examStage.Completed,
                TotalInputs = examStage.Steps.Count,
                TotalTime = examStage.Steps.Any() ? examStage.Steps.Sum(s => s.TimeBeforeStep) : null,
                TimeBeforeFirstInput = examStage.Steps.Any() ? examStage.Steps.First().TimeBeforeStep : null,
                LongestInactivityTime = examStage.Steps.Any() ? examStage.Steps.Max(s => s.TimeBeforeStep) : null
            };

            List<string> displayedInfoText = CreateBasicSummary(currentExamStageStats);
            displayedInfoText.AddRange(CreateComparisonTextWithOverallSummary(currentExamStageStats));
            //displayedInfoText.AddRange(CreateComparisonWithPreviousAttempt(currentExamStats, examSetStatSummary));

            return string.Join("\n", displayedInfoText.ToArray());
        }

        private List<string> CreateBasicSummary(ExamStageStats stats)
        {
            string timeInfo = stats.TotalTime is null ? "Not recorded (inputs were not made)." : Math.Round(stats.TotalTime.Value, 2).ToString();
            var maze = _getMazeOfExamStageQuery.GetMaze(stats.ExamStageId);
            int idealInputAmount = maze.SolutionPath.Count;

            return new List<string>
            {
                "Completed: " + (stats.Completed ? "Yes" : "No"),
                $"Total time: {timeInfo} s",
                $"Inputs made: {stats.TotalInputs}/{idealInputAmount}"
            };
        }

        private IEnumerable<string> CreateComparisonTextWithOverallSummary(ExamStageStats stats)
        {
            var examStagesOfOtherPatients = _getExamStagesQuery.GetExamStages(stats.ExamSetId, stats.Index)
                                                               .Where(e => e.Exam.Patient.Id != stats.PatientId);
            var otherCompleted = examStagesOfOtherPatients.Where(e => e.Completed);
            int totalAmountOfAttempts = examStagesOfOtherPatients.Count();
            int completedAmountOfAttempts = otherCompleted.Count();

            float averageCompleted = otherCompleted.Count() / examStagesOfOtherPatients.Count() * 100f;

            List<string> comparisonInfo = new List<string>
            {
                $"Amount of attempts by other patients: {totalAmountOfAttempts}",
                $"{averageCompleted}% of these attempts were completed ({completedAmountOfAttempts}/{totalAmountOfAttempts})",
                "",
            };

            if (stats.TotalInputs > 0 && otherCompleted.Any())
            {
                float averageInputs = (float)otherCompleted.Average(e => e.Steps.Count);
                float averageTime = (float) otherCompleted.Average(e => e.Steps.Sum(e => e.TimeBeforeStep));
                float averageTimeBeforeFirstInput = (float)otherCompleted.Average(e => e.Steps.First().TimeBeforeStep);
                float averageLongestInactivityTime = (float)otherCompleted.Average(e => e.Steps.Max(e => e.TimeBeforeStep));

                AddComparisonToAverage(averageInputs, averageTime, averageTimeBeforeFirstInput, averageLongestInactivityTime);
            }
            else if (stats.TotalInputs == 0)
            {
                comparisonInfo.Add("Patient's attempt can not be compared time-wise because it was not completed.");
            }

            return comparisonInfo;
        }

        private void AddComparisonToAverage(float averageInputs, float averageTime, float averageTimeBeforeFirstInput, float averageLongestInactivityTime)
        {
            //return new List<string> {
            //    "Comparison to average completed attempts by other patients:",
            //    $"\tTotal time: {_valueComparer.Compare(stats.TotalTime!.Value, averageTime, higherIsBetter: false)} (Avg: {averageTime})",
            //    "\tInputs made: " + _valueComparer.Compare(stats.TotalInputs, averageInputs, higherIsBetter: false),
            //comparisonInfo.Add("\tTime before first input: " + _valueComparer.Compare(stats.TimeBeforeFirstInput!.Value, averageTimeBeforeFirstInput, "Higher", "Lower"));
            //comparisonInfo.Add("\tLongest inactivity time: " + _valueComparer.Compare(stats.LongestInactivityTime!.Value, averageLongestInactivityTime, "Higher", "Lower"));
            //};
        }
    }
}