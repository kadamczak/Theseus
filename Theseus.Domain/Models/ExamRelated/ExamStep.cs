using Theseus.Domain.Models.MazeRelated.Enums;

namespace Theseus.Domain.Models.ExamRelated
{
    public class ExamStep
    {
        public Guid Id { get; set; } = default!;
        public ExamStage Stage { get; set; } = default!;
        public int Index { get; set; } = default!;
        public Direction StepTaken { get; set; } = default!;
        public float TimeBeforeStep { get; set; } = default!;

        public ExamStep() { }

        public ExamStep(Guid id)
        {
            Id = id;
        }
    }
}