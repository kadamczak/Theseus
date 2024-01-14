using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses
{
    /// <summary>
    /// The <c>DistanceGridFactory</c> class separates <c>DistanceGrid</c> creation logic.
    /// </summary>
    public class DistanceGridFactory
    {
        public DistanceGrid CreateDistanceGrid(Cell rootCell)
        {
            DistanceGrid distanceGrid = new DistanceGrid(rootCell);
            var frontier = new List<Cell>() { rootCell };

            while (frontier.Any())
            {
                var nextFrontier = new List<Cell>();
                ProcessFrontier(distanceGrid, frontier, nextFrontier);
                frontier = nextFrontier;
            }

            return distanceGrid;
        }

        private void ProcessFrontier(DistanceGrid distanceGrid, List<Cell> frontier, List<Cell> nextFrontier)
        {
            foreach (var cell in frontier)
            {
                int distanceOfLinkedCells = distanceGrid.Distance[cell] + 1;
                foreach (var linkedCell in cell.GetLinkedCells())
                {
                    AddToNextFrontierIfFirstVisit(distanceGrid, linkedCell, distanceOfLinkedCells, nextFrontier);
                }
            }
        }

        private void AddToNextFrontierIfFirstVisit(DistanceGrid distanceGrid, Cell linkedCell, int distance, List<Cell> nextFrontier)
        {
            if (!distanceGrid.DistanceAlreadyFound(linkedCell))
            {
                distanceGrid.Distance[linkedCell] = distance;
                nextFrontier.Add(linkedCell);
            }
        }
    }
}
