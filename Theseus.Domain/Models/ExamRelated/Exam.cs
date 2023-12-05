using System.ComponentModel.DataAnnotations.Schema;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Models.ExamRelated
{
    public class Exam
    {
        public Guid Id { get; set; } = default!;
        public Patient Patient { get; set; } = default!;
        public ExamSet ExamSet { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<ExamStage> Stages { get; set; } = new List<ExamStage>();

        public Exam() { }

        public Exam(Guid id)
        {
            Id = id;
        }
    }
}