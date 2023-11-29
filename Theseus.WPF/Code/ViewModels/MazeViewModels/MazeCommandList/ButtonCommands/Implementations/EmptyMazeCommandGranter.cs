using System.Collections.ObjectModel;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.ViewModels
{
    public class EmptyMazeCommandGranter : CommandGranter<MazeWithSolutionCanvasViewModel>
    {
        public override ButtonViewModel GrantCommand(ObservableCollection<CommandViewModel<MazeWithSolutionCanvasViewModel>> collection,
                                                     CommandViewModel<MazeWithSolutionCanvasViewModel> commandViewModel)
        {
            return new ButtonViewModel(false);
        }
    }
}