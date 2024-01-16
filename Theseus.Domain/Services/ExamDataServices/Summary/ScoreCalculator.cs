using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;

namespace Theseus.Domain.Services.ExamDataServices.Summary
{
    /// <summary>
    /// The <c>ScoreCalculator</c> can calculate <c>ExamStage</c> and <c>Exam</c> scores on scale from 0 to 100.
    /// </summary>
    public class ScoreCalculator
    {
        private readonly IGetMazeOfExamStageQuery _getMazeOfExamStageQuery;

        public ScoreCalculator(IGetMazeOfExamStageQuery getMazeOfExamStageQuery)
        {
            _getMazeOfExamStageQuery = getMazeOfExamStageQuery;
        }

        public double CalculateScoreForExam(Exam exam)
        {
            int amountOfStages = exam.Stages.Count;
            double scoreSum = 0;

            foreach (var stage in exam.Stages)
            {
                if (stage.Completed)
                    scoreSum += CalculateScoreForExamStage(stage);
            }

            return scoreSum / amountOfStages;
        }

        public double CalculateScoreForExamStage(ExamStage stage)
        {
            var maze = _getMazeOfExamStageQuery.GetMaze(stage.Id);
            int idealInputAmount = maze.SolutionPath.Count;
            int wrongCellAmount = stage.Steps.Where(s => !s.Correct && !s.HitWall).Count();
            int wallHitsAmount = stage.Steps.Where(s => s.HitWall).Count();
            return CalculateScoreForExamStage(idealInputAmount, wrongCellAmount, wallHitsAmount, stage.TotalTime);
        }

        public double CalculateScoreForExamStage(int idealInputAmount, int redundantInputs, int wallHits, float totalTime)
        {
            double pointsForPrecision = 70 - redundantInputs * (3 + 80.0f / (double)idealInputAmount) - 2.0f * wallHits;
            if (pointsForPrecision < 0) pointsForPrecision = 0;

            double baseTimeForMaze = 0.9 * idealInputAmount;
            double excessTime = totalTime - baseTimeForMaze;
            if (excessTime < 0) excessTime = 0;
            double pointsForTime = 30 - 30 * excessTime / baseTimeForMaze;
            if (pointsForTime < 0) pointsForTime = 0;

            return pointsForPrecision + pointsForTime;
        }
    }
}