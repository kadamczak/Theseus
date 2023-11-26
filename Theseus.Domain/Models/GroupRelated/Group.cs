using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.GroupRelated
{
    public class Group
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public StaffMember Owner { get; set; } = default!;
        public List<StaffMember> StaffMembers { get; set; } = new List<StaffMember>();
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public List<ExamSet> ExamSets { get; set; } = new List<ExamSet>();
    }
}