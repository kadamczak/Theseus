﻿using Theseus.Domain.Models.UserRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.AccountViewModels.PatientViewModels.PatientCommandList.Info.Implementations
{
    public class EmptyPatientInfoGranter : InfoGranter<Patient>
    {
        public override string GrantInfo(CommandViewModel<Patient> commandViewModel)
        {
            return string.Empty;
        }
    }
}