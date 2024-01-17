using System;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication
{
    /// <summary>
    /// The <c>StaffMemberAuthenticator</c> class defines login/register logic for <c>StaffMember</c> accounts.
    /// It manipulates the <c>ICurrentStaffMemberStore</c> according to <c>IStaffMemberAuthenticationService</c> response.
    /// </summary>
    public class StaffMemberAuthenticator : IStaffMemberAuthenticator
    {
        private readonly IStaffMemberAuthenticationService _staffMemberAuthenticationService;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;

        public StaffMemberAuthenticator(IStaffMemberAuthenticationService staffMemberAuthenticationService,
                                        ICurrentStaffMemberStore currentStaffMemberStore)
        {
            this._staffMemberAuthenticationService = staffMemberAuthenticationService;
            this._currentStaffMemberStore = currentStaffMemberStore;
        }

        public StaffMember? CurrentStaffMember
        {
            get => _currentStaffMemberStore.StaffMember;
            set
            {
                this._currentStaffMemberStore.StaffMember = value;
                StaffMemberStateChanged?.Invoke();
            }
        }

        public bool IsLoggedInAsStaffMember => _currentStaffMemberStore.IsStaffMemberLoggedIn;

        public event Action StaffMemberStateChanged;

        public async Task Login(string username, string password)
        {
            CurrentStaffMember = await _staffMemberAuthenticationService.Login(username, password);
        }

        public void Logout()
        {
            CurrentStaffMember = null;
        }

        public async Task<StaffMemberRegistrationResult> Register(StaffMember newStaffMember, string confirmPassword)
        {
            return await _staffMemberAuthenticationService.Register(newStaffMember, confirmPassword);
        }
    }
}