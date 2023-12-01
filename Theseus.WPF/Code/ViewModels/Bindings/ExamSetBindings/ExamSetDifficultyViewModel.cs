using Theseus.Domain.Models.ExamSetRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings.ExamSetBindings
{
    public class ExamSetDifficultyViewModel : LabelValueViewModel<ExamSetDifficulty>
    {
        public ExamSetDifficultyViewModel(string label, ExamSetDifficulty value) : base(label, value)
        {
        }
    }
}