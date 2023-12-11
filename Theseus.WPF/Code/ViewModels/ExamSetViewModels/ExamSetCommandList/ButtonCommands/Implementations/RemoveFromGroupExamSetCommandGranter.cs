using System;
using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Infrastructure.Queries.StaffMemberQueries;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations
{
    public class RemoveFromGroupExamSetCommandGranter : CommandGranter<ExamSet>
    {
        private readonly IRemoveExamSetFromGroupCommand _removeExamSetFromGroupCommand;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupDetailsStore;
        private readonly IGetOwnerOfGroupQuery _getOwnerOfGroupQuery;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;

        public RemoveFromGroupExamSetCommandGranter(IRemoveExamSetFromGroupCommand removeExamSetFromGroupCommand,
                                                    SelectedModelDetailsStore<Group> selectedGroupDetailsStore,
                                                    IGetOwnerOfGroupQuery getOwnerOfGroupQuery,
                                                    ICurrentStaffMemberStore currentStaffMemberStore)
        {
            _removeExamSetFromGroupCommand = removeExamSetFromGroupCommand;
            _selectedGroupDetailsStore = selectedGroupDetailsStore;
            _getOwnerOfGroupQuery = getOwnerOfGroupQuery;
            _currentStaffMemberStore = currentStaffMemberStore;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamSet>> collection,
                                                     CommandViewModel<ExamSet> commandViewModel)
        {
            return CurrentStaffMemberCanRemoveExamSets() ?
                new ButtonViewModel(show: true, "Remove".Resource(), new RemoveExamSetFromGroupCommand(collection, commandViewModel, _removeExamSetFromGroupCommand, _selectedGroupDetailsStore)) :
                new ButtonViewModel(show: false);
        }

        private bool CurrentStaffMemberCanRemoveExamSets()
        {
            Group currentGroup = _selectedGroupDetailsStore.SelectedModel;
            Guid _groupOwnerId = _getOwnerOfGroupQuery.GetOwner(currentGroup.Id).Id;
            return _currentStaffMemberStore.StaffMember.Id == _groupOwnerId;
        }
    }
}