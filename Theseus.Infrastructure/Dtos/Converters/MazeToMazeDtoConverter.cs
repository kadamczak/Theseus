using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
using Theseus.Infrastructure.Extensions;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class MazeToMazeDtoConverter
    {
        static Direction[] directions = new Direction[2] { Direction.East, Direction.South };

        public static MazeDto Convert(Maze mazeGrid)
        {
            byte[] mazeCells = new byte[mazeGrid.CellAmount];

            foreach (var (cell, index) in mazeGrid.WithIndex())
            {
                mazeCells[index] = ConvertCellToByte(cell);
            }

            return new MazeDto(height: mazeGrid.RowAmount, width: mazeGrid.ColumnAmount, data: mazeCells);
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
