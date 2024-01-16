namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamStats
{
    /// <summary>
    /// The <c>ExamStats</c> class contains analysis info of one <c>Exam</c>.
    /// </summary>
    public class ExamStats
    {
        public Guid PatientId { get; set; }
        public Guid GroupId { get; set; }
        public Guid ExamSetId { get; set; }

        public DateTime CreatedAt { get; set; }
        public double Score { get; set; }
        public int AttemptNumber { get; set; }

        public bool NoSkips { get; set; }
        public float TotalExamTime { get; set; }
        public int CompletedMazeAmount { get; set; }
        public int TotalInputs { get; set; }
        public int RedundantInputs { get; set; }
        public int WallHits { get; set; }
        public int MazeAmount { get; set; }
        public int IdealStepAmount { get; set; }
    }
}