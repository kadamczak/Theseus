using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamTransitionViewModel : ViewModelBase
    {
        public ICommand GoToNextPage { get; }

        public ExamTransitionViewModel(NavigationService<ExamPageViewModel> examPageNavigationService)
        {
            GoToNextPage = new NavigateCommand<ExamPageViewModel>(examPageNavigationService);
        }
    }
}