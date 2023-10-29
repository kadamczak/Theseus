using System.Collections.Generic;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Stores
{
    public class MazeListStore
    {
        public IEnumerable<MazeWithSolution> MazesInList { get; set; } = new List<MazeWithSolution>();
    }
}