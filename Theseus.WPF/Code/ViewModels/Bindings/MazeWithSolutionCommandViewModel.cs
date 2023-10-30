using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class MazeWithSolutionCommandViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        public ICommand Command { get; set; }

        private string _commandName;
        public string CommandName
        {
            get => _commandName;
            set
            {
                _commandName = value;
                OnPropertyChanged(nameof(CommandName));
            }
        }

        private bool _selected = false;
        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        public MazeWithSolutionCommandViewModel(MazeWithSolution mazeWithSolution)
        {
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(mazeWithSolution);
        }
    }
}
