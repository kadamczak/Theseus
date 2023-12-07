using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.DataViewModels;

namespace Theseus.WPF.Code.ViewModels
{
    public class ViewDataViewModel : ViewModelBase
    {
        public PatientsExamsSummaryViewModel PatientsExamsSummaryViewModel { get; set; }

        public ICommand GoToRecentExams { get; }

        public ViewDataViewModel(PatientsExamsSummaryViewModel patientsExamsSummaryViewModel,
                                 NavigationService<RecentExamsViewModel> recentExamsNavigationService)
        {
            PatientsExamsSummaryViewModel = patientsExamsSummaryViewModel;
            GoToRecentExams = new NavigateCommand<RecentExamsViewModel>(recentExamsNavigationService);
        }
    }
}