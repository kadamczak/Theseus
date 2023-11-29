using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.SetViewModels;

namespace Theseus.WPF.Code.ViewModels
{
    public class CreateSetViewModel : ViewModelBase
    {
        public SetGeneratorViewModel SetGeneratorViewModel { get; }
        public ICommand NavigateToCreateSetManually { get; }

        public CreateSetViewModel(SetGeneratorViewModel setGeneratorViewModel,
                                  MazesInExamSetStore mazesInExamSetStore,
                                  NavigationService<CreateSetManuallyViewModel> createSetManuallyNavigationService)
        {
            mazesInExamSetStore.SelectedMazes.Clear();

            this.SetGeneratorViewModel = setGeneratorViewModel;
            NavigateToCreateSetManually = new NavigateCommand<CreateSetManuallyViewModel>(createSetManuallyNavigationService);
        }
    }
}