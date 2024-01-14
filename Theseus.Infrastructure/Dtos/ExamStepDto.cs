namespace Theseus.Infrastructure.Dtos
{
    /// <summary>
    /// Class representing <c>ExamStep</c> structure as a database entry.
    /// </summary>
    public class ExamStepDto
    {
        public Guid Id { get; set; } = default!;
        public ExamStageDto StageDto { get; set; } = default!;
        public int Index { get; set; } = default!;
        public int StepTaken { get; set; } = default!;
        public float TimeBeforeStep { get; set; } = default!;
        public bool Correct { get; set; } = default!;
        public bool HitWall { get; set; } = default!;

        public ExamStepDto() { }

        public ExamStepDto(Guid id)
        {
            Id = id;
        }
    }
}