using System;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.ButtonCommands
{
    public class StaffMemberCommandGranterFactory : CommandGranterFactory<StaffMember, StaffMemberButtonCommand>
    {
        private readonly EmptyStaffMemberCommandGranter _emptyCommandGranter;
        private readonly RemoveStaffMemberCommandGranter _removeCommandGranter;

        public StaffMemberCommandGranterFactory(EmptyStaffMemberCommandGranter emptyCommandGranter, RemoveStaffMemberCommandGranter removeCommandGranter)
        {
            _emptyCommandGranter = emptyCommandGranter;
            _removeCommandGranter = removeCommandGranter;
        }

        public override CommandGranter<StaffMember> Get(StaffMemberButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                StaffMemberButtonCommand.None => _emptyCommandGranter,
                StaffMemberButtonCommand.Remove => _removeCommandGranter,
                _ => throw new ArgumentException("Invalid staff member command type.")
            };
        }
    }
}