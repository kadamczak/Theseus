using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class MazeToMazeDtoConverter
    {
        static Direction[] directions = new Direction[2] { Direction.East, Direction.South };

        public static MazeDto Convert(Maze maze)
        {
            byte[] mazeCells = new byte[maze.CellAmount];

            foreach (var (cell, index) in maze.WithIndex())
            {
                mazeCells[index] = ConvertCellToByte(cell);
            }

            return new MazeDto(height: maze.RowAmount, width: maze.ColumnAmount, data: mazeCells);
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
            return cell.IsLinkedToNeighbour(direction) ? (byte)direction : (byte)0;
        }
    }
}
