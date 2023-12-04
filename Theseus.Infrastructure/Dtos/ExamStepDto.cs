namespace Theseus.Infrastructure.Dtos
{
    public class ExamStepDto
    {
        public Guid Id { get; set; } = default!;
        public ExamStageDto Stage { get; set; } = default!;
        public int Index { get; set; } = default!;
        public int StepTaken { get; set; } = default!;
        public float TimeBeforeStep { get; set; } = default!;

        public ExamStepDto() { }

        public ExamStepDto(Guid id)
        {
            Id = id;
        }
    }
}