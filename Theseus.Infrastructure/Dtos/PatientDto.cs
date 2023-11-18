using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class PatientDto
    {
        [Key]
        public Guid Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public int? Age { get; set; }
        public string Sex { get; set; }
        public string ProfessionType { get; set; }
        public string EducationLevel { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public virtual GroupDto GroupDto { get; set; } = default!;
    }
}