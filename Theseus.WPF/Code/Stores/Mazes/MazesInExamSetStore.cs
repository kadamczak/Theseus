using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Stores.Mazes
{
    public class MazesInExamSetStore
    {
        public ObservableCollection<MazeWithSolution> SelectedMazes = new ObservableCollection<MazeWithSolution>();
    }
}