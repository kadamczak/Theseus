using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands.Implementations
{
    public class RemovePatientCommandGranter : CommandGranter<Patient>
    {
        private readonly IRemovePatientFromGroupCommand _removePatientFromGroupCommand;

        public RemovePatientCommandGranter(IRemovePatientFromGroupCommand removePatientFromGroupCommand)
        {
            _removePatientFromGroupCommand = removePatientFromGroupCommand;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Patient>> collection, CommandViewModel<Patient> commandViewModel)
        {
            return new ButtonViewModel(true, "Remove", new RemovePatientFromGroupCommand(collection, commandViewModel, _removePatientFromGroupCommand));
        }
    }
}