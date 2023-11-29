using System.Collections.ObjectModel;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands.Implementations
{
    public class EmptyPatientCommandGranter : CommandGranter<Patient>
    {
        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Patient>> collection, CommandViewModel<Patient> commandViewModel)
        {
            return new ButtonViewModel(show: false);
        }
    }
}
