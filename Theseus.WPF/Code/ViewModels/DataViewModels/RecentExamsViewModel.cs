using System.Windows.Input;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Exams;
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
                                    ICurrentStaffMemberStore currentStaffMemberStore,
                                    IGetExamsOfStaffMemberQuery getExamsQuery,
                                    ExamSetStatCalculator examSetStatCalculator,
                                    ExamSetStatsStore examSetStatsStore,
                                    SelectedModelListStore<Exam> selectedExamListStore,
                                    NavigationService<ViewDataViewModel> viewDataNavigationService)
        {
            LoadExamsOfCurrentStaffMember(getExamsQuery, currentStaffMemberStore, selectedExamListStore);
            examSetStatCalculator.Calculate(calculateAverages: false);

            GoBack = new NavigateCommand<ViewDataViewModel>(viewDataNavigationService);

            ExamCommandListViewModel = examCommandListViewModelFactory.Create(ExamButtonCommand.None, ExamButtonCommand.None, ExamInfo.GeneralInfo);
            ExamCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadExamsOfCurrentStaffMember(IGetExamsOfStaffMemberQuery getExamsQuery,
                                                   ICurrentStaffMemberStore currentStaffMemberStore,
                                                   SelectedModelListStore<Exam> selectedExamListStore)
        {
            var exams = getExamsQuery.GetExams(currentStaffMemberStore.StaffMember.Id);
            selectedExamListStore.ModelList = exams;
        }
    }
}