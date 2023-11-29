using System.Collections.ObjectModel;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels.MazeViewModels.MazeCommandList.ButtonCommands.Implementations
{
    public class AddToExamSetCommandGranter : CommandGranter<MazeWithSolutionCanvasViewModel>
    {
        private readonly MazesInExamSetStore _mazeInExamSetStore;

        public AddToExamSetCommandGranter(MazesInExamSetStore mazeInExamSetStore)
        {
            _mazeInExamSetStore = mazeInExamSetStore;
        }

        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> collection,
                                                     CommandViewModel<MazeWithSolutionCanvasViewModel> commandViewModel)
        {
            return new ButtonViewModel(true,
                                       "Add",
                                       new AddToExamSetCommand(commandViewModel, _mazeInExamSetStore));
        }
    }
}