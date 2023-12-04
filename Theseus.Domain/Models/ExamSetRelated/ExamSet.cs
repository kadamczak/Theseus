using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.ExamSetRelated
{
    public class ExamSet
    {
        public Guid Id { get; set; } = default;
        public string Name { get; set; } = default!;

        public List<ExamSetMazeIndex> ExamSetMazeIndexes { get; set; } = new List<ExamSetMazeIndex>();

        public StaffMember StaffMember { get; set; } = default!;
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Exam> Exams { get; set; } = new List<Exam>();

        public ExamSet() { }

        public ExamSet(Guid id)
        {
            Id = id;
        }
    }
}