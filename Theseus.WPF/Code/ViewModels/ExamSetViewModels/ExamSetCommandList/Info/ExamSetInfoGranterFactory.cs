using System;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info.Implementations;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.Info
{
    public class ExamSetInfoGranterFactory : InfoGranterFactory<ExamSet, ExamSetInfo>
    {
        private readonly EmptyExamSetInfoGranter _emptyInfoGranter;
        private readonly GeneralExamSetInfoGranter _generalInfoGranter;
        private readonly OwnerExamSetInfoGranter _ownerInfoGranter;

        public ExamSetInfoGranterFactory(EmptyExamSetInfoGranter emptyInfoGranter,
                                         GeneralExamSetInfoGranter generalInfoGranter,
                                         OwnerExamSetInfoGranter ownerInfoGranter)
        {
            _emptyInfoGranter = emptyInfoGranter;
            _generalInfoGranter = generalInfoGranter;
            _ownerInfoGranter = ownerInfoGranter;
        }

        public override InfoGranter<ExamSet> Create(ExamSetInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                ExamSetInfo.None => _emptyInfoGranter,
                ExamSetInfo.GeneralInfo => _generalInfoGranter,
                ExamSetInfo.OwnerInfo => _ownerInfoGranter,
                _ => throw new ArgumentException("Invalid exam set info type.")
            };
        }
    }
}