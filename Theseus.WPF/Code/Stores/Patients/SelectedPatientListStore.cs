using System.Collections.Generic;
using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.Stores.Patients
{
    public class SelectedPatientListStore
    {
        public IEnumerable<Patient> Patients { get; set; }
    }
}