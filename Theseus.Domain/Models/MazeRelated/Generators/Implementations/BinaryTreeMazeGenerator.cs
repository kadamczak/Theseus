using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.Generators.Implementations
{
    public class BinaryTreeMazeGenerator : MazeGeneratorBase
    {
        public BinaryTreeMazeGenerator() { }

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
            int index = rnd.Next(0, cellNeighbours.Count());
            Cell randomCell = cellNeighbours.ElementAt(index);

            currentCell.LinkTo(randomCell);
        }
    }
}
