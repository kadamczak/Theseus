using System.ComponentModel.DataAnnotations;

namespace Theseus.Infrastructure.Dtos
{
    public class StaffMemberDto
    {
        [Key]
        public Guid Id { get; set; } = default!;
        public string Username { get; } = default!;
        public string PasswordHash { get; } = default!;
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime DateCreated { get; } = DateTime.Now;

        public ICollection<PatientDto> PatientDtos { get; set; } = default!;
        //public ICollection<MazeDto> MazeDtos { get; set; } = default!;
    }
}
