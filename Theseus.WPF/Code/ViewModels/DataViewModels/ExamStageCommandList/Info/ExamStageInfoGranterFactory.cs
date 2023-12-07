using System;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info.Implementations;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.Info
{
    public class ExamStageInfoGranterFactory : InfoGranterFactory<ExamStageWithMazeViewModel, ExamStageInfo>
    {
        private readonly EmptyExamStageInfoGranter _emptyInfoGranter;
        private readonly GeneralExamStageInfoGranter _generalInfoGranter;

        public ExamStageInfoGranterFactory(EmptyExamStageInfoGranter emptyInfoGranter, GeneralExamStageInfoGranter generalInfoGranter)
        {
            _emptyInfoGranter = emptyInfoGranter;
            _generalInfoGranter = generalInfoGranter;
        }

        public override InfoGranter<ExamStageWithMazeViewModel> Create(ExamStageInfo chosenInfoType)
        {
            return chosenInfoType switch
            {
                ExamStageInfo.None => _emptyInfoGranter,
                ExamStageInfo.GeneralInfo => _generalInfoGranter,
                _ => throw new ArgumentException("Invalid exam stage info type.")
            };
        }
    }
}