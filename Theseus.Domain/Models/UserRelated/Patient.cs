using System.ComponentModel.DataAnnotations;
using Theseus.Domain.Models.UserRelated.Enums;

namespace Theseus.Domain.Models.UserRelated
{
    public class Patient
    {
        public Guid Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public int? Age { get; set; }
        public Sex? Sex { get; set; }
        public ProfessionType? ProfessionType { get; set; }
        public EducationLevel? EducationLevel { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<StaffMember> StaffMembers { get; set; } = default!;
    }
}