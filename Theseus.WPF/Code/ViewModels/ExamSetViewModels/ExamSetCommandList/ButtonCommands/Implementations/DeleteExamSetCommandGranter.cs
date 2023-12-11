using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.ExamSetViewModels.ExamSetCommandList.ButtonCommands.Implementations
{
    public class DeleteExamSetCommandGranter : CommandGranter<ExamSet>
    {
        private readonly IDeleteExamSetCommand _removeExamSetCommand;
        private readonly IGetExamsOfExamSetQuery _getExamsQuery;

        public DeleteExamSetCommandGranter(IDeleteExamSetCommand removeExamSetCommand, IGetExamsOfExamSetQuery getExamsQuery)
        {
            _removeExamSetCommand = removeExamSetCommand;
            _getExamsQuery = getExamsQuery;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<ExamSet>> collection, CommandViewModel<ExamSet> commandViewModel)
        {
            return new ButtonViewModel(true, "Delete".Resource(), new DeleteExamSetCommand(collection, commandViewModel, _removeExamSetCommand, _getExamsQuery));
        }
    }
}