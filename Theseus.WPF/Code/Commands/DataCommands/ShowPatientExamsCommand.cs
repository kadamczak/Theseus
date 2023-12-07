using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.DataCommands
{
    public class ShowPatientExamsCommand : CommandBase
    {
        private readonly CommandViewModel<Patient> _patientCommandViewModel;
        private readonly SelectedModelDetailsStore<Patient> _patientDetailsStore;
        private readonly NavigationService<PatientExamsViewModel> _patientExamsNavigationService;

        public ShowPatientExamsCommand(CommandViewModel<Patient> patientCommandViewModel,
                                       SelectedModelDetailsStore<Patient> patientDetailsStore,
                                       NavigationService<PatientExamsViewModel> patientExamsNavigationService)
        {
            _patientCommandViewModel = patientCommandViewModel;
            _patientDetailsStore = patientDetailsStore;
            _patientExamsNavigationService = patientExamsNavigationService;
        }

        public override void Execute(object? parameter)
        {
            Patient patient = _patientCommandViewModel.Model;
            _patientDetailsStore.SelectedModel = patient;
            _patientExamsNavigationService.Navigate();
        }
    }
}