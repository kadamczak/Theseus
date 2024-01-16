using Theseus.Domain.Models.ExamRelated;

namespace Theseus.Domain.Services.ExamDataServices.Summary.ExamSetGroup
{
    public class ExamSetGroupStatSummary
    {
        public Guid ExamSetId { get; set; }
        public Guid GroupId { get; set; }

        public IEnumerable<Exam> Exams { get; set; } = new List<Exam>();

        public int MazeAmount { get; set; }
        public int IdealStepAmount { get; set; }

        public float? AverageTotalTime { get; set; }
        public float? AverageTotalInputs { get; set; }
        public float? AverageTotalRedundantInputs { get; set; }
        public float? AverageTotalWallHits { get; set; }
        public float? AverageCompletedMazes { get; set; }
    }
}