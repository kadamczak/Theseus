using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeStructure;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class MazeDtoToMazeConverter
    {
        static Direction[] directions = new Direction[2] { Direction.South, Direction.East };

        public static Maze Convert(MazeDto mazeDto)
        {
            Maze maze = new Maze(mazeDto.Height, mazeDto.Width, mazeDto.Id);

            foreach (var (cell, index) in maze.WithIndex())
            {
                byte cellValue = mazeDto.Data[index];

                LinkToNeighboursWhoseBitIsSetToOne(cell, cellValue);
            }

            return maze;
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
