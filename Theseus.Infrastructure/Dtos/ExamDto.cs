namespace Theseus.Infrastructure.Dtos
{
    /// <summary>
    /// Class representing <c>Exam</c> structure as a database entry.
    /// </summary>
    public class ExamDto
    {
        public Guid Id { get; set; } = default!;
        public PatientDto PatientDto { get; set; } = default!;
        public ExamSetDto ExamSetDto { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<ExamStageDto> StageDtos { get; set; } = default!;

        public ExamDto() { }

        public ExamDto(Guid id)
        {
            Id = id;
        }
    }
}