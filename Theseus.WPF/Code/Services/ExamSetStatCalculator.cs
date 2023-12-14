using System;
using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Exams;

namespace Theseus.WPF.Code.Services
{
    public class ExamSetStatCalculator
    {
        private readonly ExamSetStatsStore _examSetStatsStore;
        private readonly SelectedModelListStore<Exam> _selectedExamListStore;
        private readonly IGetMazeOfExamStageQuery _getMazeOfExamStageQuery;

        private readonly IGetExamsOfGroupOfExamSetQuery _getExamsQuery;
        private readonly IGetOrderedMazesWithSolutionOfExamSetQuery _getMazesQuery;

        public ExamSetStatCalculator(ExamSetStatsStore examSetStatsStore,
                                     SelectedModelListStore<Exam> selectedExamListStore,
                                     IGetExamsOfGroupOfExamSetQuery getExamsQuery,
                                     IGetMazeOfExamStageQuery getMazeOfExamStageQuery,
                                     IGetOrderedMazesWithSolutionOfExamSetQuery getMazesQuery)
        {
            _examSetStatsStore = examSetStatsStore;
            _selectedExamListStore = selectedExamListStore;
            _getExamsQuery = getExamsQuery;
            _getMazeOfExamStageQuery = getMazeOfExamStageQuery;
            _getMazesQuery = getMazesQuery;
        }

        public void Calculate(bool calculateAverages = true)
        {
            _examSetStatsStore.ExamSetStatList.Clear();

            foreach (var exam in _selectedExamListStore.ModelList)
            {
                Guid examSetId = exam.ExamSet.Id;
                Guid groupId = exam.Patient.Group.Id;

                if (!ExamSetStatsInThisGroupAlreadyCalculated(_examSetStatsStore.ExamSetStatList, examSetId, groupId))
                {
                    ExamSetStatSummary statSummary = CalculateSummaryForGroupForExamSet(examSetId, groupId, calculateAverages);
                    _examSetStatsStore.ExamSetStatList.Add(statSummary);
                }
            }
        }

        public ExamSetStatSummary CalculateSummaryForGroupForExamSet(Guid examSetId, Guid groupId, bool calculateAverages = true)
        {
            var mazesInExamSet = _getMazesQuery.GetMazesWithSolution(examSetId);

            ExamSetStatSummary statSummary = new ExamSetStatSummary
            {
                ExamSetId = examSetId,
                GroupId = groupId,
                MazeAmount = mazesInExamSet.Count(),
                IdealStepAmount = mazesInExamSet.Sum(m => m.SolutionPath.Count),
                Exams = _getExamsQuery.GetExams(groupId, examSetId)
            };

            if (!calculateAverages)
                return statSummary;

            var examsWithNoSkippedMazes = statSummary.Exams.Where(s => !s.Stages.Where(s => !s.Completed).Any());
            statSummary.AverageCompletedMazes = (float) statSummary.Exams.Average(e => e.Stages.Count(s => s.Completed));

            if (examsWithNoSkippedMazes.Any())
            {
                statSummary.AverageTotalInputs = (float) examsWithNoSkippedMazes.Average(e => e.Stages.Sum(s => s.Steps.Count));
                statSummary.AverageTotalTime = (float) examsWithNoSkippedMazes.Average(e => e.Stages.Sum(s => s.Steps.Sum(s => s.TimeBeforeStep)));
            }

            return statSummary;
        }

        private bool ExamSetStatsInThisGroupAlreadyCalculated(List<ExamSetStatSummary> examSetStatsList, Guid examSetId, Guid groupId)
            => examSetStatsList.Where(e => e.ExamSetId == examSetId && e.GroupId == groupId).Any();   
        
        public double CalculateScoreForExam(Exam exam)
        {
            int amountOfStages = exam.Stages.Count;
            double scoreSum = 0;

            foreach (var stage in exam.Stages)
            {
                if (stage.Completed)
                {
                    scoreSum += CalculateScoreForExamStage(stage);
                }
            }

            return scoreSum / amountOfStages;
        }
        
        public double CalculateScoreForExamStage(ExamStage stage)
        {
            var maze = _getMazeOfExamStageQuery.GetMaze(stage.Id);
            int idealInputAmount = maze.SolutionPath.Count;
            float totalTime = stage.Steps.Sum(s => s.TimeBeforeStep);
            int inputAmount = stage.Steps.Count();
            return CalculateScoreForExamStage(idealInputAmount, inputAmount, totalTime);
        }

        public double CalculateScoreForExamStage(int idealInputAmount, int inputAmount, float totalTime)
        {
            int excessInputs = inputAmount - idealInputAmount;
            double pointsForPrecision = 70 - 70 * (double)excessInputs / idealInputAmount - excessInputs * 2;
            if (pointsForPrecision < 0) pointsForPrecision = 0;

            double baseTimeForMaze = 0.25 * idealInputAmount;
            double excessTime = totalTime - baseTimeForMaze;
            if (excessTime < 0) excessTime = 0;
            double pointsForTime = 30 - 30 * excessTime / baseTimeForMaze;
            if (pointsForTime < 0) pointsForTime = 0;

            return pointsForPrecision + pointsForTime;
        }
    }
}