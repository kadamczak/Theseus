using System;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication;

namespace Theseus.WPF.Code.Stores.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IStaffMemberAuthenticationService _staffMemberAuthenticationService;
        private readonly ICurrentUser _currentUser;

        public Authenticator(IStaffMemberAuthenticationService staffMemberAuthenticationService, ICurrentUser currentUser)
        {
            this._staffMemberAuthenticationService = staffMemberAuthenticationService;
            this._currentUser = currentUser;
        }


        public StaffMember CurrentStaffMember => throw new NotImplementedException();

        public bool IsLoggedIn => throw new NotImplementedException();

        public event Action StateChanged;

        public Task LoginStaffMember(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void LogoutStaffMember()
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResult> RegisterStaffMember(StaffMember newStaffMember, string confirmPassword)
        {
            throw new NotImplementedException();
        }
    }
}