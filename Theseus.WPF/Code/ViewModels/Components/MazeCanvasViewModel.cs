using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeCanvasViewModel : ViewModelBase
    {
        public Maze Maze { get; }

        public MazeCanvasViewModel(Maze grid)
        {
            this.Maze = grid;
        }
    }
}