using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeSolutionGenerators.HelperClasses
{
    public class DistanceGridFactory
    {
        public DistanceGrid CreateDistanceGrid(Cell rootCell, int rows, int columns)
        {
            DistanceGrid distanceGrid = new DistanceGrid(rootCell, rows, columns);
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
                foreach (var linkedCell in cell.LinkedCells)
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
