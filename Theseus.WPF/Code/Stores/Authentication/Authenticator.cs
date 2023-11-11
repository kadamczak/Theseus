using System;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication;

namespace Theseus.WPF.Code.Stores.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IStaffMemberAuthenticationService _staffMemberAuthenticationService;
        private readonly ICurrentUserStore _currentUserStore;

        public Authenticator(IStaffMemberAuthenticationService staffMemberAuthenticationService, ICurrentUserStore currentUserStore)
        {
            this._staffMemberAuthenticationService = staffMemberAuthenticationService;
            this._currentUserStore = currentUserStore;
        }


        public StaffMember? CurrentStaffMember
        {
            get => _currentUserStore.CurrentStaffMember;
            set
            {
                this._currentUserStore.CurrentStaffMember = value;
                StaffMemberStateChanged?.Invoke();
            }
        }

        public bool IsLoggedInAsStaffMember => CurrentStaffMember is not null;

        public event Action StaffMemberStateChanged;

        public async Task LoginStaffMember(string username, string password)
        {
            CurrentStaffMember = await _staffMemberAuthenticationService.Login(username, password);
        }

        public void LogoutStaffMember()
        {
            CurrentStaffMember = null;
        }

        public async Task<RegistrationResult> RegisterStaffMember(StaffMember newStaffMember, string confirmPassword)
        {
            return await _staffMemberAuthenticationService.Register(newStaffMember, confirmPassword);
        }
    }
}