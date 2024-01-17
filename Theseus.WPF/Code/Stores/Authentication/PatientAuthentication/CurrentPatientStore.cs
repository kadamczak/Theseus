using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication.PatientAuthentication
{
    /// <summary>
    /// The <c>CurrentPatientStore</c> class stores the currently logged-in <c>Patient</c>.
    /// </summary>
    public class CurrentPatientStore : ICurrentPatientStore
    {
        private Patient? _patient;
        public Patient? Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
                PatientStateChanged?.Invoke();
            }
        }

        public bool IsPatientLoggedIn => Patient is not null;
        public event Action PatientStateChanged;
    }
}