using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication
{
    public interface ICurrentUser
    {
        StaffMember CurrentStaffMember { get; set; }
        event Action StateChanged;
    }
}