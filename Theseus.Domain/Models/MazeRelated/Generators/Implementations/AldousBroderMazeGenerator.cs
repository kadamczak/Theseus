using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.Generators.Implementations
{
    public class AldousBroderMazeGenerator : MazeGeneratorBase
    {
        public override void ApplyAlgorithm(Maze maze, Random rnd)
        {
            Cell currentCell = maze.GetRandomCell();
            int unvisitedCellAmount = maze.CellAmount - 1;

            while (unvisitedCellAmount > 0)
            {
                Cell randomNeighbour = GetRandomNeighbour(currentCell, rnd);

                if (IsUnvisited(randomNeighbour))
                {
                    currentCell.LinkTo(randomNeighbour);
                    unvisitedCellAmount--;
                }

                currentCell = randomNeighbour;
            }
        }

        private Cell GetRandomNeighbour(Cell cell, Random rnd)
        {
            var neighbours = cell.GetAdjecentCells();
            return neighbours.GetRandomItem(rnd);
        }

        private bool IsUnvisited(Cell cell) => !cell.LinkedCells.Any();
    }
}
