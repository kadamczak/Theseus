using System;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;
using Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands.Implementations;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands
{
    public class ExamCommandGranterFactory : CommandGranterFactory<Exam, ExamButtonCommand>
    {
        private readonly EmptyExamCommandGranter _emptyGranter;

        public ExamCommandGranterFactory(EmptyExamCommandGranter emptyGranter)
        {
            _emptyGranter = emptyGranter;
        }

        public override CommandGranter<Exam> Get(ExamButtonCommand chosenCommandType)
        {
            return chosenCommandType switch
            {
                ExamButtonCommand.None => _emptyGranter,
                _ => throw new ArgumentException("Invalid exam command type.")
            };
        }
    }
}