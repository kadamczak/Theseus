using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Domain.Models.MazeRelated.Generators.Implementations
{
    public class SidewinderMazeGenerator : MazeGeneratorBase
    {
        public SidewinderMazeGenerator() { }

        public override void ApplyAlgorithm(Maze mazeGrid, Random rnd)
        {
            foreach (var row in mazeGrid.IterateRows())
            {
                List<Cell> cellRun = new List<Cell>();

                foreach (var cell in row)
                {
                    cellRun.Add(cell);

                    if (CheckIfShouldEndRun(cell, rnd))
                        this.EndCellRun(cellRun, rnd); //Links North
                    else
                        cell.LinkToNeighbour(Direction.East);
                }
            }
        }


        private bool CheckIfShouldEndRun(Cell currentCell, Random rnd)
        {
            if (!currentCell.HasNeighbour(Direction.East))
            {
                return true;
            }
            else if (currentCell.HasNeighbour(Direction.North) && ShouldEraseNorthBorder(rnd))
            {
                return true;
            }

            return false;
        }

        private bool ShouldEraseNorthBorder(Random rnd)
        {
            return rnd.Next(0, 2) == 1;
        }

        private void EndCellRun(List<Cell> cellRun, Random rnd)
        {
            int index = rnd.Next(0, cellRun.Count);
            Cell randomCell = cellRun[index];

            Cell? northCell = randomCell.AdjecentCellSpaces[Direction.North];

            if (northCell is not null)
                randomCell.LinkTo(northCell);

            cellRun.Clear();
        }
    }
}
