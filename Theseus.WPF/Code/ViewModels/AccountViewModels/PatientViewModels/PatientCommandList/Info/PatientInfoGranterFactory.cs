using System;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info
{
    public class PatientInfoGranterFactory : InfoGranterFactory<Patient, PatientInfo>
    {
        private readonly EmptyPatientInfoGranter _emptyInfoGranter;

        public PatientInfoGranterFactory(EmptyPatientInfoGranter emptyInfoGranter)
        {
            _emptyInfoGranter = emptyInfoGranter;
        }

        public override InfoGranter<Patient> Create(PatientInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                PatientInfo.None => _emptyInfoGranter,
                _ => throw new ArgumentException("Invalid patient info type.")
            };
        }
    }
}