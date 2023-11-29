using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations
{
    public class DeleteExamSetCommandGranter : CommandGranter<ExamSet>
    {
        private readonly IDeleteExamSetCommand _removeExamSetCommand;

        public DeleteExamSetCommandGranter(IDeleteExamSetCommand removeExamSetCommand)
        {
            _removeExamSetCommand = removeExamSetCommand;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamSet>> collection, CommandViewModel<ExamSet> commandViewModel)
        {
            return new ButtonViewModel(true, "Delete", new DeleteExamSetCommand(collection, commandViewModel, _removeExamSetCommand));
        }
    }
}