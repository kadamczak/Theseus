using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication
{
    public interface ICurrentStaffMemberStore
    {
        StaffMember? StaffMember { get; set; }
        public bool IsStaffMemberLoggedIn { get; }
        event Action StaffMemberStateChanged;
    }
}
