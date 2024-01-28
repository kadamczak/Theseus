using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Models.MazeRelated.MazeStructureGenerators.Implementations
{
    /// <summary>
    /// The <c>SidewinderMazeStructureGenerator</c> class applies the Sidewinder algorithm on <c>Maze</c>.
    /// </summary>
    public class SidewinderMazeStructureGenerator : MazeStructureGeneratorBase
    {
        public override void GenerateMazeStructureInGrid(Maze mazeGrid, int? rndSeed = null)
        {
            Random rnd = CreateRandom(rndSeed);

            foreach (var row in mazeGrid.IterateRows())
            {
                List<Cell> cellRun = new List<Cell>();

                foreach (var cell in row)
                {
                    cellRun.Add(cell);

                    if (ShouldEndCellRun(cell, rnd))
                    {
                        this.EndCellRun(cellRun, rnd); //Links North
                    }
                    else
                    {
                        cell.LinkTo(Direction.East);
                    }
                }
            }
        }


        private bool ShouldEndCellRun(Cell currentCell, Random rnd)
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

        private bool ShouldEraseNorthBorder(Random rnd) => rnd.Next(0, 2) == 1;

        private void EndCellRun(List<Cell> cellRun, Random rnd)
        {
            Cell randomCell = cellRun.GetRandomItem(rnd);
            Cell? northCell = randomCell.GetAdjecentCellSpace(Direction.North);

            if (northCell is not null)
            {
                randomCell.LinkTo(northCell);
            }

            cellRun.Clear();
        }
    }
}
