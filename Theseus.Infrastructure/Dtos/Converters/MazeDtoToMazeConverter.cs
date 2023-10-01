using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
using Theseus.Infrastructure.Extensions;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class MazeDtoToMazeConverter
    {
        static Direction[] directions = new Direction[2] { Direction.South, Direction.East };

        public static Maze Convert(MazeDto mazeDto)
        {
            Maze mazeGrid = new Maze(mazeDto.Height, mazeDto.Width);

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
