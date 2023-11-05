using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class PatientDto
    {
        [Key]
        public Guid Id { get; set; } = default!;
        public string Username { get; } = default!;
        public int? Age { get; set; }
        public string? Sex { get; set; }
        public string? ProfessionType { get; set; }
        public string? EducationLevel { get; set; }
        public DateTime DateCreated { get; } = DateTime.Now;

        public ICollection<StaffMemberDto> StaffMemberDtos { get; set; } = default!;
    }
}