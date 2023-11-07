using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        private StaffMember _currentStaffMember;
        public StaffMember CurrentStaffMember
        {
            get
            {
                return _currentStaffMember;
            }
            set
            {
                _currentStaffMember = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;
    }
}