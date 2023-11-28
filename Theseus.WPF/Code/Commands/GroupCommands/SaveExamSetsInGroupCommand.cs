using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.ExamSets;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class SaveExamSetsInGroupCommand : AsyncCommandBase
    {
        private readonly IChangeExamSetsOfStaffMemberInGroupCommand _changeExamSetsOfUserInGroupCommand;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly SelectedModelDetailsStore<Group> _selectedGroupStore;
        private readonly ExamSetsInGroupStore _examSetsInGroupStore;
        private readonly NavigationService<GroupDetailsViewModel> _groupDetailsNavigationService;

        public SaveExamSetsInGroupCommand(IChangeExamSetsOfStaffMemberInGroupCommand changeExamSetsOfUserInGroupCommand,
                                          ICurrentStaffMemberStore currentStaffMemberStore,
                                          SelectedModelDetailsStore<Group> selectedGroupStore,
                                          ExamSetsInGroupStore examSetsInGroupStore,
                                          NavigationService<GroupDetailsViewModel> groupDetailsNavigationService)
        {
            _changeExamSetsOfUserInGroupCommand = changeExamSetsOfUserInGroupCommand;
            _currentStaffMemberStore = currentStaffMemberStore;
            _selectedGroupStore = selectedGroupStore;
            _examSetsInGroupStore = examSetsInGroupStore;
            _groupDetailsNavigationService = groupDetailsNavigationService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Guid staffMemberId = _currentStaffMemberStore.StaffMember.Id;
            List<ExamSet> newExamSets = _examSetsInGroupStore.SelectedExamSets.ToList();
            Guid groupId = _selectedGroupStore.SelectedModel.Id;

            await _changeExamSetsOfUserInGroupCommand.ChangeExamSets(newExamSets, groupId, staffMemberId);

            _groupDetailsNavigationService.Navigate();
        }
    }
}
