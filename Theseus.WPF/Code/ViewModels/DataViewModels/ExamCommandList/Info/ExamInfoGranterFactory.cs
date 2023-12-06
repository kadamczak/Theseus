using System;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info
{
    public class ExamInfoGranterFactory : InfoGranterFactory<Exam, ExamInfo>
    {
        private readonly EmptyExamInfoGranter _emptyGranter;
        private readonly GeneralExamInfoGranter _generalGranter;

        public ExamInfoGranterFactory(EmptyExamInfoGranter emptyGranter, GeneralExamInfoGranter generalGranter)
        {
            _emptyGranter = emptyGranter;
            _generalGranter = generalGranter;
        }

        public override InfoGranter<Exam> Create(ExamInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                ExamInfo.None => _emptyGranter,
                ExamInfo.GeneralInfo => _generalGranter,
                _ => throw new ArgumentException("Invalid exam info type.")
            };
        }
    }
}