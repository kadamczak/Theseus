using System;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands.Implementations;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands
{
    public class ExamStageCommandGranterFactory : CommandGranterFactory<ExamStage, ExamStageButtonCommand>
    {
        private readonly EmptyExamStageCommandGranter _emptyCommandGranter;

        public ExamStageCommandGranterFactory(EmptyExamStageCommandGranter emptyCommandGranter)
        {
            _emptyCommandGranter = emptyCommandGranter;
        }

        public override CommandGranter<ExamStage> Get(ExamStageButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                ExamStageButtonCommand.None => _emptyCommandGranter,
                _ => throw new ArgumentException("Invalid exam stage command type.")
            }; ;
        }
    }
}