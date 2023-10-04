using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.Generators.Implementations
{
    public class BinaryTreeMazeGenerator : MazeGeneratorBase
    {
        public override void ApplyAlgorithm(Maze mazeGrid, Random rnd)
        {
            foreach (var cell in mazeGrid)
            {
                var cellNeighbours = cell.GetAdjecentCells(Direction.North, Direction.East);

                if (cellNeighbours.Any())
                    LinkToRandomAdjecentCell(cell, cellNeighbours, rnd);
            }
        }

        private void LinkToRandomAdjecentCell(Cell currentCell, IEnumerable<Cell> cellNeighbours, Random rnd)
        {
            Cell randomCell = cellNeighbours.GetRandomItem(rnd);
            currentCell.LinkTo(randomCell);
        }
    }
}
