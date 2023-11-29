using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info
{
    public class PatientInfoGranterFactory : InfoGranterFactory<Patient, PatientInfo>
    {
        public override InfoGranter<Patient> Create(PatientInfo chosenInfoType)
        {
            throw new System.NotImplementedException();
        }
    }
}