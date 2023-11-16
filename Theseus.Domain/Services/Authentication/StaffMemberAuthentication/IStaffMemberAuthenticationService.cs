using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Services.Authentication.StaffMemberAuthentication
{
    public interface IStaffMemberAuthenticationService
    {
        Task<StaffMemberRegistrationResult> Register(StaffMember newAccount, string confirmPassword);
        Task<StaffMember> Login(string username, string password);
    }
}