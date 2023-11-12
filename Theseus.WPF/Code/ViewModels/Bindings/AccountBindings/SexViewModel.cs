using Theseus.Domain.Models.UserRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings.AccountBindings
{
    public class SexViewModel : LabelValueViewModel<Sex>
    {
        public SexViewModel(string label, Sex value) : base(label, value)
        {
        }
    }
}