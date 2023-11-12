using Theseus.Domain.Models.UserRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class EducationLevelViewModel : LabelValueViewModel<EducationLevel>
    {
        public EducationLevelViewModel(string label, EducationLevel value) : base(label, value)
        {
        }
    }
}
