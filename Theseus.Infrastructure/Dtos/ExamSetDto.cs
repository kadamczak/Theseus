using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Infrastructure.Dtos
{
    public class ExamSetDto
    {
        [Key]
        public Guid Id { get; set; } = default;
        public IEnumerable<MazeDto> MazeDtos { get; set; } = default!;
        //public StaffMember StaffMember { get; set; } = default!;

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