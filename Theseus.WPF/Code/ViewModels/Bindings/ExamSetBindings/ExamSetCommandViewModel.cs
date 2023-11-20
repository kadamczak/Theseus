using Theseus.Domain.Models.ExamSetRelated;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class ExamSetCommandViewModel : CommandViewModel
    {
        public ExamSet ExamSet { get; }

        public ExamSetCommandViewModel(ExamSet examSet)
        {
            ExamSet = examSet;
        }
    }
}