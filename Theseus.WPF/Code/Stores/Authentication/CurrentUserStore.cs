using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication
{
    public class CurrentUserStore : ICurrentUserStore
    {
        private StaffMember? _currentStaffMember;
        public StaffMember? CurrentStaffMember
        {
            get
            {
                return _currentStaffMember;
            }
            set
            {
                _currentStaffMember = value;
                StaffMemberStateChanged?.Invoke();
            }
        }

        private Patient? _currentPatient;
        public Patient? CurrentPatient
        {
            get
            {
                return _currentPatient;
            }
            set
            {
                _currentPatient = value;
                PatientStateChanged?.Invoke();
            }
        }

        public event Action StaffMemberStateChanged;
        public event Action PatientStateChanged;
    }
}