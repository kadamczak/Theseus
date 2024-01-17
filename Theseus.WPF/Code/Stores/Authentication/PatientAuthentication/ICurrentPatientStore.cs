using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication.PatientAuthentication
{
    /// <summary>
    /// The <c>ICurrentPatientStore</c> defines currently logged-in <c>Patient</c> storage.
    /// </summary>
    public interface ICurrentPatientStore
    {
        Patient? Patient { get; set; }
        public bool IsPatientLoggedIn { get; }
        event Action PatientStateChanged;
    }
}