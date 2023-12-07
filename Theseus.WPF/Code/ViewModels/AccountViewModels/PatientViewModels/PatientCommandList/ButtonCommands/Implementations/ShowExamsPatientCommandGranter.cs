using System.Collections.ObjectModel;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Commands.DataCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands.Implementations
{
    public class ShowExamsPatientCommandGranter : CommandGranter<Patient>
    {
        private readonly SelectedModelDetailsStore<Patient> _patientDetailsStore;
        private readonly NavigationService<PatientExamsViewModel> _patientExamsNavigationService;

        public ShowExamsPatientCommandGranter(SelectedModelDetailsStore<Patient> patientDetailsStore, NavigationService<PatientExamsViewModel> patientExamsNavigationService)
        {
            _patientDetailsStore = patientDetailsStore;
            _patientExamsNavigationService = patientExamsNavigationService;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Patient>> collection, CommandViewModel<Patient> commandViewModel)
        {
            return new ButtonViewModel(show: true,
                                      "Exams",
                                      new ShowPatientExamsCommand(commandViewModel, _patientDetailsStore, _patientExamsNavigationService));
        }
    }
}