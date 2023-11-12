using Theseus.Domain.Models.UserRelated.Enums;

namespace Theseus.WPF.Code.ViewModels.Bindings.AccountBindings
{
    public class ProfessionTypeViewModel : LabelValueViewModel<ProfessionType>
    {
        public ProfessionTypeViewModel(string label, ProfessionType value) : base(label, value)
        {
        }
    }
}