using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamEndViewModel : ViewModelBase
    {
        public ICommand GoBack { get; }

        public ExamEndViewModel(NavigationService<BeginTestViewModel> navigationService)
        {          
            GoBack = new NavigateCommand<BeginTestViewModel>(navigationService);
        }
    }
}