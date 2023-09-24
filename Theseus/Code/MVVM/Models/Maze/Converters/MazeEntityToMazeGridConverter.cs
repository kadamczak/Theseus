using Theseus.Code.Extensions;
using Theseus.Code.MVVM.Models.Maze.Entity;
using Theseus.Code.MVVM.Models.Maze.Enums;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.MVVM.Models.Maze.Converters
{
    public class MazeEntityToMazeGridConverter
    {
        static Direction[] directions = new Direction[4] { Direction.South, Direction.East, Direction.North, Direction.West };

        public static MazeGrid Convert(MazeEntity mazeEntity)
        {
            MazeGrid mazeGrid = new MazeGrid(mazeEntity.Height, mazeEntity.Width);
            
            foreach (var (cell, index) in mazeGrid.WithIndex())
            {
                byte cellValue = mazeEntity.Data[index];

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
