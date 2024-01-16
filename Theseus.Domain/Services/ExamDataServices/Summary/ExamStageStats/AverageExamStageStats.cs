namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamStageStats
{
    /// <summary>
    /// The <c>AverageExamStats</c> class contains analysis info of multiple <c>ExamStage</c>s.
    /// </summary>
    public class AverageExamStageStats
    {
        public int TotalAttemptAmount { get; set; }
        public int CompletedAttemptAmount { get; set; }

        public float TotalInputs { get; set; }
        public float? TotalTime { get; set; }
        public float? TimeBeforeFirstInput { get; set; }
        public float? LongestInactivityTime { get; set; }
        public float? RedundantInputs { get; set; }
        public float? WallHits { get; set; }
    }
}