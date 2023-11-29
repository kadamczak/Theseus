using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList
{
    public class PatientCommandListViewModelFactory
    {
        private readonly SelectedModelListStore<Patient> _selectedListStore;
        private readonly PatientCommandGranterFactory _commandGranterFactory;
        private readonly PatientInfoGranterFactory _infoGranterFactory;

        public PatientCommandListViewModelFactory(SelectedModelListStore<Patient> selectedListStore,
                                                  PatientCommandGranterFactory commandGranterFactory,
                                                  PatientInfoGranterFactory infoGranterFactory)
        {
            _selectedListStore = selectedListStore;
            _commandGranterFactory = commandGranterFactory;
            _infoGranterFactory = infoGranterFactory;
        }

        public PatientCommandListViewModel Create(PatientButtonCommand command1Type, PatientButtonCommand command2Type, PatientInfo infoType)
        {
            return new PatientCommandListViewModel(_selectedListStore, _commandGranterFactory, _infoGranterFactory, command1Type, command2Type, infoType);
        }
    }
}