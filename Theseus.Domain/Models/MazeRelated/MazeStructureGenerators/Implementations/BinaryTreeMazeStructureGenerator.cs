using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations
{
    /// <summary>
    /// The <c>BinaryMazeStructureGenerator</c> class applies the Binary algorithm on <c>Maze</c>.
    /// </summary>
    public class BinaryTreeMazeStructureGenerator : MazeStructureGeneratorBase
    {
        public override void GenerateMazeStructureInGrid(Maze mazeGrid, int? rndSeed = null)
        {
            Random rnd = CreateRandom(rndSeed);
            foreach (var cell in mazeGrid)
            {
                var cellNeighbours = cell.GetAdjecentCells(Direction.North, Direction.East);

                if (cellNeighbours.Any())
                {
                    LinkToRandomAdjecentCell(cell, cellNeighbours, rnd);
                }
            }
        }

        private void LinkToRandomAdjecentCell(Cell currentCell, IEnumerable<Cell> cellNeighbours, Random rnd)
        {
            Cell randomCell = cellNeighbours.GetRandomItem(rnd);
            currentCell.LinkTo(randomCell);
        }
    }
}
