using Theseus.Domain.Models.UserRelated;

namespace Theseus.WPF.Code.ViewModels.Bindings.AccountBindings
{
    public class PatientCommandViewModel : CommandViewModel
    {
        public Patient Patient { get; }

        public PatientCommandViewModel(Patient patient)
        {
            Patient = patient;
        }
    }
}