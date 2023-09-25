using Theseus.Code.Extensions;
using Theseus.Code.MVVM.Models.Maze.Dto;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models.Maze.Converters
{
    public class MazeDtoToMazeGridConverter
    {
        static Direction[] directions = new Direction[2] { Direction.South, Direction.East };

        public static MazeGrid Convert(MazeDto mazeDto)
        {
            MazeGrid mazeGrid = new MazeGrid(mazeDto.Height, mazeDto.Width);
            
            foreach (var (cell, index) in mazeGrid.WithIndex())
            {
                byte cellValue = mazeDto.Data[index];

                LinkToNeighboursWhoseBitIsSetToOne(cell, cellValue);
            }

            return mazeGrid;
        }

        private static void LinkToNeighboursWhoseBitIsSetToOne(Cell cell, byte cellValue)
        {
            int bitPosition = 0;
            foreach (var direction in directions)
            {
                if (IsBitSetToOne(cellValue, bitPosition))
                {
                    cell.LinkToNeighbour(direction);
                }

                bitPosition++;
            }
        }

        private static bool IsBitSetToOne(byte value, int position)
        {
            return (value & (1 << position)) != 0;
        }
    }
}
