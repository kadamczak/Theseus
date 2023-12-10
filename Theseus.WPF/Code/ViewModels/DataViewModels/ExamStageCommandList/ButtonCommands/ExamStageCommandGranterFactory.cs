using System;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.Bindings.ExamBindings;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands.Implementations;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamStageCommandList.ButtonCommands
{
    public class ExamStageCommandGranterFactory : CommandGranterFactory<ExamStageWithMazeViewModel, ExamStageButtonCommand>
    {
        private readonly EmptyExamStageCommandGranter _emptyCommandGranter;
        private readonly ShowDetailsExamStageCommandGranter _showDetailsCommandGranter;

        public ExamStageCommandGranterFactory(EmptyExamStageCommandGranter emptyCommandGranter, ShowDetailsExamStageCommandGranter showDetailsCommandGranter)
        {
            _emptyCommandGranter = emptyCommandGranter;
            _showDetailsCommandGranter = showDetailsCommandGranter;
        }

        public override CommandGranter<ExamStageWithMazeViewModel> Get(ExamStageButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                ExamStageButtonCommand.None => _emptyCommandGranter,
                ExamStageButtonCommand.ShowDetails => _showDetailsCommandGranter,
                _ => throw new ArgumentException("Invalid exam stage command type.")
            }; ;
        }
    }
}