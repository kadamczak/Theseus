using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.ExamCommandInterfaces;
using Theseus.Domain.Models.ExamRelated;
using Theseus.WPF.Code.Commands.ExamCommands;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.DataViewModels.ExamCommandList.ButtonCommands.Implementations
{
    public class DeleteExamCommandGranter : CommandGranter<Exam>
    {
        private readonly IDeleteExamCommand _deleteExamCommand;

        public DeleteExamCommandGranter(IDeleteExamCommand deleteExamCommand)
        {
            _deleteExamCommand = deleteExamCommand;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<Exam>> collection, CommandViewModel<Exam> commandViewModel)
        {
            return new ButtonViewModel(true, "Delete", new DeleteExamCommand(collection, commandViewModel, _deleteExamCommand));
        }
    }
}