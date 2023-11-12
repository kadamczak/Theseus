using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.SetRelated;

namespace Theseus.Domain.Models.UserRelated
{
    public class StaffMember
    {
        public Guid Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        //public ICollection<MazeWithSolution> MazesWithSolutions { get; set; } = default!;
        //public ICollection<ExamSet> ExamSets { get; set; } = default!;
    }
}