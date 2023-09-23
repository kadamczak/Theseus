using Theseus.Code.Extensions;
using Theseus.Code.MVVM.Models.Maze.Entity;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models.Maze
{

    public class MazeFormatConverter
    {
        public static MazeEntity MazeGridToMazeEntity(MazeGrid mazeGrid)
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

            cellAsByte += AddValueIfLinkExists(cell, Direction.West,  value: 8);
            cellAsByte += AddValueIfLinkExists(cell, Direction.North, value: 4);
            cellAsByte += AddValueIfLinkExists(cell, Direction.East,  value: 2);
            cellAsByte += AddValueIfLinkExists(cell, Direction.South, value: 1);

            return cellAsByte;
        }

        private static byte AddValueIfLinkExists(Cell cell, Direction direction, byte value)
        {
            Cell? neighbourCell = cell.AdjecentCellSpaces[direction];

            return (cell.IsLinked(neighbourCell)) ? value : (byte)0;
        }

        public static MazeGrid MazeEntityToMazeGrid(MazeEntity mazeEntity)
        {

            return null;
        }

    }
}
