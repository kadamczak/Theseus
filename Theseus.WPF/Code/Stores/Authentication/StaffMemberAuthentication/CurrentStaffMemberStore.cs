﻿using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication
{
    /// <summary>
    /// The <c>CurrentStaffMemberStore</c> class stores the currently logged-in <c>StaffMember</c>.
    /// </summary>
    public class CurrentStaffMemberStore : ICurrentStaffMemberStore
    {
        private StaffMember? _StaffMember;
        public StaffMember? StaffMember
        {
            get
            {
                return _StaffMember;
            }
            set
            {
                _StaffMember = value;
                StaffMemberStateChanged?.Invoke();
            }
        }

        public bool IsStaffMemberLoggedIn => StaffMember is not null;
        public event Action StaffMemberStateChanged;
    }
}