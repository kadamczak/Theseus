using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    /// <summary>
    /// Class representing <c>ExamSet</c> structure as a database entry.
    /// </summary>
    public class ExamSetDto
    {
        [Key]
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default!;

        public virtual ICollection<ExamSetDto_MazeDto> ExamSetDto_MazeDto { get; set; } = new List<ExamSetDto_MazeDto>();

        public virtual StaffMemberDto Owner { get; set; } = default!;
        public virtual ICollection<GroupDto> GroupDtos { get; set; } = default!;
        public virtual ICollection<ExamDto> ExamDtos { get; set; } = default!;

        public ExamSetDto() { }

        public ExamSetDto(Guid id)
        {
            Id = id;
        }
    }
}