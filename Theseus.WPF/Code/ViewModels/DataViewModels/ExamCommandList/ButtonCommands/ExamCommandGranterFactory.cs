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

        public ExamCommandGranterFactory(EmptyExamCommandGranter emptyGranter,
                                         DeleteExamCommandGranter deleteGranter)
        {
            _emptyGranter = emptyGranter;
            _deleteGranter = deleteGranter;
        }

        public override CommandGranter<Exam> Get(ExamButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                ExamButtonCommand.None => _emptyGranter,
                ExamButtonCommand.Delete => _deleteGranter,
                _ => throw new ArgumentException("Invalid exam command type.")
            };
        }
    }
}