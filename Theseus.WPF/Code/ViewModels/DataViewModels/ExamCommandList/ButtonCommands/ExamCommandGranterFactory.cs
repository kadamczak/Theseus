using System;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands.Implementations;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands
{
    public class ExamCommandGranterFactory : CommandGranterFactory<Exam, ExamButtonCommand>
    {
        private readonly EmptyExamCommandGranter _emptyGranter;
        private readonly DeleteExamCommandGranter _deleteGranter;
        private readonly ShowDetailsExamCommandGranter _showDetailsGranter;

        public ExamCommandGranterFactory(EmptyExamCommandGranter emptyGranter,
                                         DeleteExamCommandGranter deleteGranter,
                                         ShowDetailsExamCommandGranter showDetailsGranter)
        {
            _emptyGranter = emptyGranter;
            _deleteGranter = deleteGranter;
            _showDetailsGranter = showDetailsGranter;
        }

        public override CommandGranter<Exam> Get(ExamButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                ExamButtonCommand.None => _emptyGranter,
                ExamButtonCommand.Delete => _deleteGranter,
                ExamButtonCommand.ViewStages => _showDetailsGranter,
                _ => throw new ArgumentException("Invalid exam command type.")
            };
        }
    }
}