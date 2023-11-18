using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class ExamSetDto
    {
        [Key]
        public Guid Id { get; set; } = default;
        public virtual ICollection<MazeDto> MazeDtos { get; set; } = default!;
        public virtual StaffMemberDto Owner { get; set; } = default!;
        public virtual ICollection<GroupDto> GroupDtos { get; set; } = default!;

        public ExamSetDto() { }

        public ExamSetDto(Guid id)
        {
            Id = id;
        }

        public ExamSetDto(Guid id, ICollection<MazeDto> mazeDtos)
        {
            Id = id;
            MazeDtos = mazeDtos;
        }
    }
}