using System;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication
{
    public interface IStaffMemberAuthenticator
    {
        StaffMember? CurrentStaffMember { get; }
        bool IsLoggedInAsStaffMember { get; }
        event Action StaffMemberStateChanged;

        Task<StaffMemberRegistrationResult> Register(StaffMember newStaffMember, string confirmPassword);
        Task Login(string username, string password);
        void Logout();
    }
}