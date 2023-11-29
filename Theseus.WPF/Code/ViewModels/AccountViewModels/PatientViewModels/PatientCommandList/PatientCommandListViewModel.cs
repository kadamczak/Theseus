using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientCommandListViewModel : CommandListViewModel<Patient, PatientButtonCommand, PatientInfo>
    {
        public PatientCommandListViewModel(SelectedModelListStore<Patient> selectedModelListStore,
                                           CommandGranterFactory<Patient, PatientButtonCommand> commandGranterFactory,
                                           InfoGranterFactory<Patient, PatientInfo> infoGranterFactory,
                                           PatientButtonCommand command1,
                                           PatientButtonCommand command2,
                                           PatientInfo info) : base(selectedModelListStore, commandGranterFactory, infoGranterFactory, command1, command2, info)
        {
        }
    }
}