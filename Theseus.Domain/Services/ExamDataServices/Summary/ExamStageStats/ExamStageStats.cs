namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamStageStats
{
    public class ExamStageStats
    {
        public Guid PatientId { get; set; }
        public Guid GroupId { get; set; }
        public Guid ExamStageId { get; set; }
        public Guid ExamSetId { get; set; }
        public int Index { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Completed { get; set; }

        public int TotalInputs { get; set; }
        public float TotalTime { get; set; }
        public float? TimeBeforeFirstInput { get; set; }
        public float? LongestInactivityTime { get; set; }
        public int RedundantInputs { get; set; }
        public int WallHits { get; set; }
    }
}