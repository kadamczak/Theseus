using System.Collections.ObjectModel;
using Theseus.Domain.CommandInterfaces.MazeCommandInterfaces;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    public class DeleteMazeCommandGranter : CommandGranter<MazeWithSolutionCanvasViewModel>
    {
        private readonly IDeleteMazeWithSolutionCommand _removeMazeCommand;
        private readonly IGetExamSetsWithMazeQuery _getExamSetsWithMazeQuery;

        public DeleteMazeCommandGranter(IDeleteMazeWithSolutionCommand removeMazeCommand, IGetExamSetsWithMazeQuery getExamSetsWithMazeQuery)
        {
            _removeMazeCommand = removeMazeCommand;
            _getExamSetsWithMazeQuery = getExamSetsWithMazeQuery;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> collection,
                                                     CommandViewModel<MazeWithSolutionCanvasViewModel> commandViewModel)
        {
            return new ButtonViewModel(true,
                                       "Delete".Resource(),
                                       new DeleteMazeCommand(collection, commandViewModel, _removeMazeCommand, _getExamSetsWithMazeQuery));
        }
    }
}