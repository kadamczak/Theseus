using System.Collections.Generic;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.StaffMembers
{
    public class SelectedStaffMemberListStore
    {
        public IEnumerable<StaffMember> StaffMembers { get; set; }
    }
}