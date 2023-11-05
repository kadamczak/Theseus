using System.ComponentModel.DataAnnotations;

namespace Theseus.Domain.Models.UserRelated
{
    public class StaffMember
    {
        [Key]
        public Guid Id { get; } = default;
        public string Username { get; } = default!;
        public string PasswordHash { get; } = default!;
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime DateCreated { get; } = DateTime.Now;
    }
}