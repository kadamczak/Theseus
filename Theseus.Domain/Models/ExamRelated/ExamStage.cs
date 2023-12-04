namespace Theseus.Domain.Models.ExamRelated
{
    public class ExamStage
    {
        public Guid Id { get; set; } = default!;
        public Exam Exam { get; set; } = default!;
        public int Index { get; set; } = 0;

        public List<ExamStep> Steps { get; set; } = new List<ExamStep>();
        public bool Completed { get; set; } = false;

        public ExamStage() { }

        public ExamStage(Guid id)
        {
            Id = id;
        }
    }
}