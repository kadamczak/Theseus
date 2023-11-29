using System;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands
{
    public class ExamSetCommandGranterFactory : CommandGranterFactory<ExamSet, ExamSetButtonCommand>
    {
        private readonly EmptyExamSetCommandGranter _emptyCommandGranter;
        private readonly DeleteExamSetCommandGranter _deleteCommandGranter;
        private readonly RemoveFromGroupExamSetCommandGranter _removeFromGroupCommandGranter;
        private readonly ShowDetailsExamSetCommandGranter _showDetailsCommandGranter;
        private readonly AddToGroupExamSetCommandGranter _addToGroupCommandGranter;

        public ExamSetCommandGranterFactory(EmptyExamSetCommandGranter emptyCommandGranter,
                                            DeleteExamSetCommandGranter deleteCommandGranter,
                                            RemoveFromGroupExamSetCommandGranter removeFromGroupCommandGranter,
                                            ShowDetailsExamSetCommandGranter showDetailsCommandGranter,
                                            AddToGroupExamSetCommandGranter addToGroupCommandGranter)
        {
            _emptyCommandGranter = emptyCommandGranter;
            _deleteCommandGranter = deleteCommandGranter;
            _removeFromGroupCommandGranter = removeFromGroupCommandGranter;
            _showDetailsCommandGranter = showDetailsCommandGranter;
            _addToGroupCommandGranter = addToGroupCommandGranter;
        }

        public override CommandGranter<ExamSet> Get(ExamSetButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                ExamSetButtonCommand.None => _emptyCommandGranter,
                ExamSetButtonCommand.ShowDetails => _showDetailsCommandGranter,
                ExamSetButtonCommand.AddToGroup => _addToGroupCommandGranter,
                ExamSetButtonCommand.RemoveFromGroup => _removeFromGroupCommandGranter,
                ExamSetButtonCommand.Delete => _deleteCommandGranter,
                _ => throw new ArgumentException("Invalid exam set command type.")
            };
        }
    }
}