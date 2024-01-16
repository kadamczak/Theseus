namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamStats
{
    public class AverageExamStats
    {
        public int TotalExamAmount { get; set; }
        public int ExamsWithNoSkipsAmount { get; set; }
        public float AverageCompletedMazes { get; set; }
        public float? AverageTotalTime { get; set; }
        public float? AverageTotalInputs { get; set; }
        public float? AverageReduntantInputs { get; set; }
        public float? AverageWallHits { get; set; }
    }
}