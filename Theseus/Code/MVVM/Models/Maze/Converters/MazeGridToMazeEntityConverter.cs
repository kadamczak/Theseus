using Theseus.Code.Extensions;
using Theseus.Code.MVVM.Models.Maze.Entity;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models.Maze.Converters
{

    public class MazeGridToMazeEntityConverter
    {
        static Direction[] directions = new Direction[4] { Direction.West, Direction.North, Direction.East, Direction.South };

        public static MazeEntity Convert(MazeGrid mazeGrid)
        {
            byte[] mazeCells = new byte[mazeGrid.CellAmount];

            foreach (var (cell, index) in mazeGrid.WithIndex())
            {
                mazeCells[index] = ConvertCellToByte(cell);
            }

            return new MazeEntity(height: mazeGrid.RowAmount, width: mazeGrid.ColumnAmount, data: mazeCells);
        }

        private static byte ConvertCellToByte(Cell cell)
        {
            byte cellAsByte = 0b0000_0000;

            foreach (var direction in directions)
            {
                cellAsByte += GetEnumValueIfLinkExists(cell, direction);
            }

            return cellAsByte;
        }

        private static byte GetEnumValueIfLinkExists(Cell cell, Direction direction)
        {
            return cell.IsLinkedToNeighbour(direction) ? (byte) direction : (byte) 0;
        }

    }
}
