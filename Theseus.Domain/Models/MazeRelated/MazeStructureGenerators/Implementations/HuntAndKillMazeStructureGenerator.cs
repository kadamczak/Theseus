using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations
{
    public class HuntAndKillMazeStructureGenerator : MazeStructureGeneratorBase
    {
        public override void GenerateMazeStructureInGrid(Maze mazeGrid)
        {
            Cell? currentCell = mazeGrid.GetRandomCell();
            Random rnd = new Random();

            while (currentCell is not null)
            {
                var unvisitedNeighbours = GetNeighbours(currentCell, predicate: neighbour => !HasBeenVisited(neighbour));

                if (unvisitedNeighbours.Any())
                {
                    Cell randomUnvisitedNeighbour = unvisitedNeighbours.GetRandomItem(rnd);
                    currentCell.LinkTo(randomUnvisitedNeighbour);
                    currentCell = randomUnvisitedNeighbour;
                }
                else
                {
                    currentCell = HuntForNewCurrentCell(mazeGrid, rnd);
                }
            }
        }

        private bool HasBeenVisited(Cell cell) => cell.LinkedCells.Any();

        private IEnumerable<Cell> GetNeighbours(Cell cell, Func<Cell, bool> predicate) => cell.GetAdjecentCells().Where(predicate);

        private Cell? HuntForNewCurrentCell(Maze mazeGrid, Random rnd)
        {
            foreach (var cell in mazeGrid)
            {
                if (!HasBeenVisited(cell))
                {
                    var visitedNeighbours = GetNeighbours(cell, predicate: HasBeenVisited);

                    if(visitedNeighbours.Any())
                    {
                        Cell randomVisitedNeighbour = visitedNeighbours.GetRandomItem(rnd);
                        cell.LinkTo(randomVisitedNeighbour);
                        return cell;
                    }
                }
            }

            return null;
        }
    }
}