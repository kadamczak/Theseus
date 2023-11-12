using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Services.Authentication.StaffMemberAuthentication
{
    public interface IStaffMemberAuthenticationService
    {
        Task<RegistrationResult> Register(StaffMember newAccount, string confirmPassword);
        Task<StaffMember> Login(string username, string password);
    }
}