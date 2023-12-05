using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class ViewDataViewModel : ViewModelBase
    {
        public ICommand GoToRecentExams { get; }

        public ViewDataViewModel(NavigationService<RecentExamsViewModel> recentExamsNavigationService)
        {
            GoToRecentExams = new NavigateCommand<RecentExamsViewModel>(recentExamsNavigationService);
        }
    }
}