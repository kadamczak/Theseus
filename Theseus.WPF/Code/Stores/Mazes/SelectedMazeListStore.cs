using System.Collections.Generic;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Stores.Mazes
{
    public class SelectedMazeListStore
    {
        public IEnumerable<MazeWithSolution> Mazes { get; set; } = new List<MazeWithSolution>();
    }
}