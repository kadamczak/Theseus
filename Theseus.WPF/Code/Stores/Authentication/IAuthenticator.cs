﻿using System.Threading.Tasks;
using System;
using Theseus.Domain.Services.Authentication;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication
{
    public interface IAuthenticator
    {
        StaffMember? CurrentStaffMember { get; }
        bool IsLoggedInAsStaffMember { get; }

        event Action AuthenticationStateChanged;

        Task<RegistrationResult> RegisterStaffMember(StaffMember newStaffMember, string confirmPassword);
        Task LoginStaffMember(string username, string password);
        void LogoutStaffMember();
    }
}