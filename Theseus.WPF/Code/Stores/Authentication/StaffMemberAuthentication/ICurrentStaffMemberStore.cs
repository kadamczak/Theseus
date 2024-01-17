using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication
{
    /// <summary>
    /// The <c>ICurrentStaffMemberStore</c> defines currently logged-in <c>StaffMember</c> storage.
    /// </summary>
    public interface ICurrentStaffMemberStore
    {
        StaffMember? StaffMember { get; set; }
        public bool IsStaffMemberLoggedIn { get; }
        event Action StaffMemberStateChanged;
    }
}
