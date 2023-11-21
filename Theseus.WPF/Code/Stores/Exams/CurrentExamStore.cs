using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.WPF.Code.Stores.Exams
{
    public class CurrentExamStore
    {
        public ExamSet SelectedExamSet { get; set; }
        public int CurrentIndex { get; set; } = 0;
    }
}