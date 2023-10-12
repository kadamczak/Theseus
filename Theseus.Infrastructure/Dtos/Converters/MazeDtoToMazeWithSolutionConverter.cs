using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class MazeDtoToMazeWithSolutionConverter
    {
        static Direction[] directions = new Direction[2] { Direction.South, Direction.East };

        public static MazeWithSolution Convert(MazeDto mazeDto)
        {
            MazeWithSolution maze = new MazeWithSolution(mazeDto.Height, mazeDto.Width, mazeDto.Id);

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
