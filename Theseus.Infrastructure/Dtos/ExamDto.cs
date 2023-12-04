namespace Theseus.Infrastructure.Dtos
{
    public class ExamDto
    {
        public Guid Id { get; set; } = default!;
        public PatientDto Patient { get; set; } = default!;
        public ExamSetDto ExamSet { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<ExamStageDto> Stages { get; set; } = default!;

        public ExamDto() { }

        public ExamDto(Guid id)
        {
            Id = id;
        }
    }
}