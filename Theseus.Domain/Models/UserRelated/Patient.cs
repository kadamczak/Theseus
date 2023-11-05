using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.UserRelated.Enums;

namespace Theseus.Domain.Models.UserRelated
{
    public class Patient
    {
        [Key]
        public Guid Id { get; } = default;
        public string Username { get; } = default!;
        public int? Age { get; set; }
        public Sex? Sex { get; set; }
        public ProfessionType? ProfessionType { get; set; }
        public EducationLevel? EducationLevel { get; set; }
        public DateTime DateCreated { get; } = DateTime.Now;
    }
}