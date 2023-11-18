using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;

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

        public List<Group> Groups { get; set; } = new List<Group>();
        public List<MazeWithSolution> MazesWithSolutions { get; set; } = new List<MazeWithSolution>();
        public List<ExamSet> ExamSets { get; set; } = new List<ExamSet>();
    }
}