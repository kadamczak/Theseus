using System.Windows.Input;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    class MazeWithSolutionActionViewModel
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        public ICommand Command { get; }

        public MazeWithSolutionActionViewModel(CommandBase command)
        {
            Command = command;
        }
    }
}
