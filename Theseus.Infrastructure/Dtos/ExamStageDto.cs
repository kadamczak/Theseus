﻿namespace Theseus.Infrastructure.Dtos
{
    public class ExamStageDto
    {
        public Guid Id { get; set; } = default!;
        public ExamDto ExamDto { get; set; } = default!;
        public int Index { get; set; } = default!;

        public virtual ICollection<ExamStepDto> StepDtos { get; set; } = default!;
        public bool Completed { get; set; } = false;

        public ExamStageDto() { }

        public ExamStageDto(Guid id)
        {
            Id = id;
        }
    }
}