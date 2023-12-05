using System;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info.Implementations;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.Info
{
    public class ExamInfoGranterFactory : InfoGranterFactory<Exam, ExamInfo>
    {
        private readonly EmptyExamInfoGranter _emptyGranter;

        public ExamInfoGranterFactory(EmptyExamInfoGranter emptyGranter)
        {
            _emptyGranter = emptyGranter;
        }

        public override InfoGranter<Exam> Create(ExamInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                ExamInfo.None => _emptyGranter,
                _ => throw new ArgumentException("Invalid exam info type.")
            };
        }
    }
}