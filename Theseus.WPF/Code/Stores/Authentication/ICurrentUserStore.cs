using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication
{
    public interface ICurrentUserStore
    {
        StaffMember? CurrentStaffMember { get; set; }
        Patient? CurrentPatient { get; set; }

        event Action StaffMemberStateChanged;
        event Action PatientStateChanged;
    }
}