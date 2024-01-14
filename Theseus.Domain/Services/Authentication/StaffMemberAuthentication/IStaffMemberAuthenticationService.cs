using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.Services.Authentication.StaffMemberAuthentication
{
    /// <summary>
    /// The <c>IStaffMemberAuthenticationService</c> interface defines <c>StaffMember</c> login and registration.
    /// </summary>
    public interface IStaffMemberAuthenticationService
    {
        Task<StaffMemberRegistrationResult> Register(StaffMember newAccount, string confirmPassword);
        Task<StaffMember> Login(string username, string password);
    }
}