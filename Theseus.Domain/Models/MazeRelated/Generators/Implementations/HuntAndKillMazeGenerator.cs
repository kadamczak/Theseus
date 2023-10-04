using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.Generators.Implementations
{
    public class HuntAndKillMazeGenerator : MazeGeneratorBase
    {
        public override void ApplyAlgorithm(Maze maze, Random rnd)
        {
            Cell? currentCell = maze.GetRandomCell();

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
                    currentCell = HuntForNewCurrentCell(maze, rnd);
                }
            }
        }

        private bool HasBeenVisited(Cell cell) => cell.LinkedCells.Any();

        private IEnumerable<Cell> GetNeighbours(Cell cell, Func<Cell, bool> predicate) => cell.GetAdjecentCells().Where(predicate);

        private Cell? HuntForNewCurrentCell(Maze maze, Random rnd)
        {
            foreach (var cell in maze)
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