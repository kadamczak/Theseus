namespace Theseus.Infrastructure.Dtos
{
    /// <summary>
    /// Class representing <c>ExamStage</c> structure as a database entry.
    /// </summary>
    public class ExamStageDto
    {
        public Guid Id { get; set; } = default!;
        public ExamDto ExamDto { get; set; } = default!;
        public int Index { get; set; } = default!;

        public virtual ICollection<ExamStepDto> StepDtos { get; set; } = default!;
        public bool Completed { get; set; } = false;
        public float TotalTime { get; set; } = default!;

        public ExamStageDto() { }

        public ExamStageDto(Guid id)
        {
            Id = id;
        }
    }
}