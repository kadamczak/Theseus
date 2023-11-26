using System;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsRemoveFromGroupExamSetCommandListViewModel : CommandListViewModel<ExamSet>
    {
        private readonly SelectedModelDetailsStore<ExamSet> _selectedExamSetDetailsStore;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;
        private readonly NavigationService<ExamSetDetailsViewModel> _navigationService;
        private readonly IRemoveExamSetFromGroupCommand _removeExamSetFromGroupCommand;

        private readonly bool _currentStaffMemberCanRemoveExamSets = false;

        public ShowDetailsRemoveFromGroupExamSetCommandListViewModel(SelectedModelListStore<ExamSet> selectedModelListStore,
                                                                     SelectedModelDetailsStore<ExamSet> selectedExamSetDetailsStore,
                                                                     NavigationService<ExamSetDetailsViewModel> navigationService,
                                                                     IRemoveExamSetFromGroupCommand removeExamSetCommand,
                                                                     ICurrentStaffMemberStore currentStaffMemberStore,
                                                                     SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                                                     IGetOwnerOfGroupQuery getOwnerOfGroupQuery) : base(selectedModelListStore)
        {
            _selectedExamSetDetailsStore = selectedExamSetDetailsStore;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _navigationService = navigationService;
            _removeExamSetFromGroupCommand = removeExamSetCommand;

            Group currentGroup = selectedGroupDetailsStore.SelectedModel;
            Guid _groupOwnerId = getOwnerOfGroupQuery.GetOwner(currentGroup.Id).Id;
            _currentStaffMemberCanRemoveExamSets = currentStaffMemberStore.StaffMember.Id == _groupOwnerId;
        }

        protected override void AddModelToActionableModels(ExamSet model)
        {
            CommandViewModel<ExamSet> examSetCommandViewModel = new CommandViewModel<ExamSet>(model)
            {
                Command1Name = "Details",
                ShowCommand1 = true
            };

            examSetCommandViewModel.Command1 = new ShowExamSetDetailsCommand(examSetCommandViewModel,
                                                                             _selectedExamSetDetailsStore,
                                                                             _navigationService);
            if (_currentStaffMemberCanRemoveExamSets)
                GrantRemoveCommand(examSetCommandViewModel);

            ActionableModels.Add(examSetCommandViewModel);
        }

        private void GrantRemoveCommand(CommandViewModel<ExamSet> examSetCommandViewModel)
        {
            examSetCommandViewModel.Command2Name = "Remove";
            examSetCommandViewModel.ShowCommand2 = true;
            examSetCommandViewModel.Command2 = new RemoveExamSetFromGroupCommand(this,
                                                                                 examSetCommandViewModel,
                                                                                 _removeExamSetFromGroupCommand,
                                                                                 _selectedGroupDetailsStore);
        }
    }
}