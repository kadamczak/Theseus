using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication
{
    public interface ICurrentUserStore
    {
        StaffMember? CurrentStaffMember { get; set; }
        event Action StateChanged;
    }
}