using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated.Enums;

namespace Theseus.Domain.Models.UserRelated
{
    public class Patient
    {
        public Guid Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public int? Age { get; set; }
        public Sex Sex { get; set; }
        public ProfessionType ProfessionType { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public Group? Group { get; set; } = default!;
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}