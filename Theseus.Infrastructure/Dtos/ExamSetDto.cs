using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class ExamSetDto
    {
        [Key]
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default!;

        public virtual ICollection<ExamSetDto_MazeDto> ExamSetDto_MazeDto { get; set; } = default!;
        public virtual IEnumerable<MazeDto> MazeDtos
        {
            get { return ExamSetDto_MazeDto.Select(t => t.MazeDto); }
        }

        public virtual StaffMemberDto Owner { get; set; } = default!;
        public virtual ICollection<GroupDto> GroupDtos { get; set; } = default!;

        public ExamSetDto() { }

        public ExamSetDto(Guid id)
        {
            Id = id;
        }
    }
}