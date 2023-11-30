using System;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.StaffMemberViewModels.StaffMemberCommandList.Info
{
    public class StaffMemberInfoGranterFactory : InfoGranterFactory<StaffMember, StaffMemberInfo>
    {
        private readonly EmptyStaffMemberInfoGranter _emptyInfoGranter;

        public StaffMemberInfoGranterFactory(EmptyStaffMemberInfoGranter emptyInfoGranter)
        {
            _emptyInfoGranter = emptyInfoGranter;
        }

        public override InfoGranter<StaffMember> Create(StaffMemberInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                StaffMemberInfo.None => _emptyInfoGranter,
                _ => throw new ArgumentException("Invalid staff member info type.")
            };
        }
    }
}