using System;
using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations
{
    public class RemoveFromGroupExamSetCommandGranter : CommandGranter<ExamSet>
    {
        private readonly IRemoveExamSetFromGroupCommand _removeExamSetFromGroupCommand;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;

        private readonly bool _currentStaffMemberCanRemoveExamSets = false;

        public RemoveFromGroupExamSetCommandGranter(IRemoveExamSetFromGroupCommand removeExamSetFromGroupCommand,
                                                    SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                                    IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                                     ICurrentStaffMemberStore currentStaffMemberStore)
        {
            if (selectedGroupDetailsStore.SelectedModel is null)
                return;

            _removeExamSetFromGroupCommand = removeExamSetFromGroupCommand;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;

            Group currentGroup = selectedGroupDetailsStore.SelectedModel;
            Guid _groupOwnerId = getOwnerOfGroupQuery.GetOwner(currentGroup.Id).Id;
            _currentStaffMemberCanRemoveExamSets = currentStaffMemberStore.StaffMember.Id == _groupOwnerId;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamSet>> collection,
                                                     CommandViewModel<ExamSet> commandViewModel)
        {
            return _currentStaffMemberCanRemoveExamSets ?
                new ButtonViewModel(show: true, "Remove", new RemoveExamSetFromGroupCommand(collection, commandViewModel, _removeExamSetFromGroupCommand, _selectedGroupDetailsStore)) :
                new ButtonViewModel(show: false);
        }
    }
}