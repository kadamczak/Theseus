using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class RecentExamsViewModel : ViewModelBase
    {
        public ExamCommandListViewModel ExamCommandListViewModel { get; set; }

        public ICommand GoBack { get; }

        public RecentExamsViewModel(ExamCommandListViewModelFactory examCommandListViewModelFactory,
                                    NavigationService<ViewDataViewModel> viewDataNavigationService)
        {
            LoadExamsOfCurrentStaffMember();

            GoBack = new NavigateCommand<ViewDataViewModel>(viewDataNavigationService);

            ExamCommandListViewModel = examCommandListViewModelFactory.Create(ExamButtonCommand.None, ExamButtonCommand.None, ExamInfo.None);
            ExamCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadExamsOfCurrentStaffMember()
        {
            
        }
    }
}