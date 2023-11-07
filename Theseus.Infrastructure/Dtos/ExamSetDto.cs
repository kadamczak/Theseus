using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class ExamSetDto
    {
        [Key]
        public Guid Id { get; set; } = default;
        public virtual IEnumerable<MazeDto> MazeDtos { get; set; } = default!;
        public StaffMemberDto Owner { get; set; } = default!;

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