using System;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info;

namespace Theseus.WPF.Code.ViewModels
{
    public class SelectExamSetsInGroupViewModel : ViewModelBase
    {
        public ExamSetCommandListViewModel AddToGroupExamSetCommandListViewModel { get; }

        public ICommand SaveExamSetsInGroup { get; }
        public ICommand GoBack { get; }

        public SelectExamSetsInGroupViewModel(ExamSetCommandListViewModelFactory addToGroupExamSetCommandListViewModel,
                                              IChangeExamSetsOfStaffMemberInGroupCommand changeExamSetsOfUserInGroupCommand,
                                              IGetAllExamSetsOfStaffMemberQuery getExamSetsQuery,
                                              NavigationService<GroupDetailsViewModel> groupDetailsNavigationService,
                                              ExamSetReturnServiceStore examSetReturnServiceStore,
                                              SelectedModelListStore<ExamSet> selectedExamSetListStore,
                                              ICurrentStaffMemberStore currentStaffMemberStore,
                                              SelectedModelDetailsStore<Group> selectedGroupStore,
                                              NavigationStore navigationStore,
                                              ExamSetsInGroupStore examSetsInGroupStore,
                                              Func<SelectExamSetsInGroupViewModel> viewModelFactory)
        {
            LoadExamSetsToStore(getExamSetsQuery, currentStaffMemberStore.StaffMember.Id, selectedExamSetListStore);

            examSetReturnServiceStore.ExamSetNavigationService = new NavigationService<ViewModelBase>(navigationStore, viewModelFactory);
            SaveExamSetsInGroup = new SaveExamSetsInGroupCommand(changeExamSetsOfUserInGroupCommand, currentStaffMemberStore, selectedGroupStore, examSetsInGroupStore, groupDetailsNavigationService);
            GoBack = new NavigateCommand<GroupDetailsViewModel>(groupDetailsNavigationService);

            AddToGroupExamSetCommandListViewModel = addToGroupExamSetCommandListViewModel.Create(ExamSetButtonCommand.ShowDetails, ExamSetButtonCommand.AddToGroup, ExamSetInfo.GeneralInfo);
            AddToGroupExamSetCommandListViewModel.CreateModelCommandViewModels();
        }

        private void LoadExamSetsToStore(IGetAllExamSetsOfStaffMemberQuery query,
                                         Guid staffMemberId,
                                         SelectedModelListStore<ExamSet> selectedExamSetListStore)
        {
            var examSets = query.GetExamSets(staffMemberId);
            selectedExamSetListStore.ModelList = examSets;
        }
    }
}