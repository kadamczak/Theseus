using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication.PatientAuthentication
{
    public interface ICurrentPatientStore
    {
        Patient? Patient { get; set; }
        public bool IsPatientLoggedIn { get; }
        event Action PatientStateChanged;
    }
}