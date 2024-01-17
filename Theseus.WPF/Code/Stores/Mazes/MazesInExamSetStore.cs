using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Stores.Mazes
{
    /// <summary>
    /// The <c>MazesInExamSetStore</c> class stores currently selected <c>MazeWithSolution</c>s to be available in an <c>ExamSet</c>.
    /// </summary>
    public class MazesInExamSetStore
    {
        public ObservableCollection<MazeWithSolution> SelectedMazes = new ObservableCollection<MazeWithSolution>();
    }
}