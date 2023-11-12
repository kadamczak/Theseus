using System;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Authentication.PatientAuthentication
{
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