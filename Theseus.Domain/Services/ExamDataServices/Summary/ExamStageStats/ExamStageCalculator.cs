using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamStageStats
{
    public class ExamStageCalculator
    {

        public ExamStageStats CalculateExamStageStats(ExamStage examStage)
        {
            Exam exam = examStage.Exam;

            return new ExamStageStats()
            {
                PatientId = exam.Patient.Id,
                GroupId = exam.Patient.Group!.Id,
                ExamStageId = examStage.Id,
                ExamSetId = exam.ExamSet.Id,
                Index = examStage.Index,
                CreatedAt = exam.CreatedAt,
                Completed = examStage.Completed,

                TotalInputs = CalculateTotalInputs(examStage.Steps),
                TotalTime = examStage.TotalTime,
                TimeBeforeFirstInput = examStage.Steps.Any() ? FindTimeBeforeFirstInput(examStage.Steps): null,
                LongestInactivityTime = examStage.Steps.Any() ? FindLongestInactivityTime(examStage.Steps) : null,
                RedundantInputs = CalculateRedundantInputs(examStage.Steps),
                WallHits = CalculateWallHits(examStage.Steps)
            };
        }

        private int CalculateTotalInputs(IEnumerable<ExamStep> examSteps) => examSteps.Count();
        private float FindTimeBeforeFirstInput(IEnumerable<ExamStep> examSteps) => examSteps.First().TimeBeforeStep;
        private float FindLongestInactivityTime(IEnumerable<ExamStep> examSteps) => examSteps.Max(s => s.TimeBeforeStep);
        private int CalculateRedundantInputs(IEnumerable<ExamStep> examSteps) => examSteps.Where(s => !s.Correct && !s.HitWall).Count();
        private int CalculateWallHits(IEnumerable<ExamStep> examSteps) => examSteps.Where(s => s.HitWall).Count();

        public AverageExamStageStats CalculateAverageStats(IEnumerable<ExamStage> examStages)
        {
            var completedExamStages = examStages.Where(e => e.Completed);

            return new AverageExamStageStats()
            {
                TotalAttemptAmount = examStages.Count(),
                CompletedAttemptAmount = completedExamStages.Count(),

                TotalTime = completedExamStages.Any() ? CalculateAverageTotalTime(completedExamStages) : null,
                TotalInputs = completedExamStages.Any() ? CalculateAverageTotalInputs(completedExamStages) : 0,
                TimeBeforeFirstInput = completedExamStages.Any() ? CalculateAverageTimeBeforeFirstInput(completedExamStages) : null,
                LongestInactivityTime = completedExamStages.Any() ? CalculateAverageLongestInactivityTime(completedExamStages) : null,
                RedundantInputs = completedExamStages.Any() ? CalculateAverageRedundantInputs(completedExamStages) : null,
                WallHits = completedExamStages.Any() ? CalculateAverageWallHits(completedExamStages) : null
            };
        }

        private float CalculateAverageTotalTime(IEnumerable<ExamStage> examStages) => (float)examStages.Average(e => e.TotalTime);
        private float CalculateAverageTotalInputs(IEnumerable<ExamStage> examStages) => (float)examStages.Average(e => CalculateTotalInputs(e.Steps));
        private float CalculateAverageTimeBeforeFirstInput(IEnumerable<ExamStage> examStages) => (float)examStages.Average(e => FindTimeBeforeFirstInput(e.Steps));
        private float CalculateAverageLongestInactivityTime(IEnumerable<ExamStage> examStages) => (float)examStages.Average(e => FindLongestInactivityTime(e.Steps));
        private float CalculateAverageRedundantInputs(IEnumerable<ExamStage> examStages) => (float)examStages.Average(e => CalculateRedundantInputs(e.Steps));
        private float CalculateAverageWallHits(IEnumerable<ExamStage> examStages) => (float)examStages.Average(e => CalculateWallHits(e.Steps));
    }
}