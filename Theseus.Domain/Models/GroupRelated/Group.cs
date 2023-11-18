using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.GroupRelated
{
    public class Group
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        //public StaffMember Creator { get; set; } = default!;
        public List<StaffMember> StaffMemberDtos { get; set; } = new List<StaffMember>();
        public List<Patient> PatientDtos { get; set; } = new List<Patient>();
        public List<ExamSet> ExamSetDtos { get; set; } = new List<ExamSet>();
    }
}