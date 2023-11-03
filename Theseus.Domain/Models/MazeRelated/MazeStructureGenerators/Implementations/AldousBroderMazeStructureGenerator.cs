using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations
{
    public class AldousBroderMazeStructureGenerator : MazeStructureGeneratorBase
    {
        public override void GenerateMazeStructureInGrid(Maze mazeGrid, int? rndSeed = null)
        {
            Random rnd = CreateRandom(rndSeed);
            Cell currentCell = mazeGrid.GetRandomCell(rnd);
            int unvisitedCellAmount = mazeGrid.CellAmount - 1;

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