using System;
using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands.Implementations;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.ButtonCommands
{
    public class PatientCommandGranterFactory : CommandGranterFactory<Patient, PatientButtonCommand>
    {
        private readonly EmptyPatientCommandGranter _emptyCommandGranter;
        private readonly RemovePatientCommandGranter _removeCommandGranter;

        public PatientCommandGranterFactory(EmptyPatientCommandGranter emptyCommandGranter, RemovePatientCommandGranter removeCommandGranter)
        {
            _emptyCommandGranter = emptyCommandGranter;
            _removeCommandGranter = removeCommandGranter;
        }

        public override CommandGranter<Patient> Get(PatientButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                PatientButtonCommand.None => _emptyCommandGranter,
                PatientButtonCommand.Remove => _removeCommandGranter,
                _ => throw new ArgumentException("Invalid patient command type.")
            };
        }
    }
}