using System;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info.Implementations;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info
{
    public class PatientInfoGranterFactory : InfoGranterFactory<Patient, PatientInfo>
    {
        private readonly EmptyPatientInfoGranter _emptyInfoGranter;
        private readonly ExamPatientInfoGranter _examInfoGranter;

        public PatientInfoGranterFactory(EmptyPatientInfoGranter emptyInfoGranter, ExamPatientInfoGranter examInfoGranter)
        {
            _emptyInfoGranter = emptyInfoGranter;
            _examInfoGranter = examInfoGranter;
        }

        public override InfoGranter<Patient> Create(PatientInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                PatientInfo.None => _emptyInfoGranter,
                PatientInfo.ExamInfo => _examInfoGranter,
                _ => throw new ArgumentException("Invalid patient info type.")
            };
        }
    }
}