using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.WPF.Code.Stores.ExamSets
{
    public class SelectedExamSetStore
    {
        public ExamSet SelectedExamSet { get; set; }
        public int CurrentMazeIndex { get; set; } = 0;
    }
}
