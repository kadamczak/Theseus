using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class MazeWithSolutionToMazeDtoConverter
    {
        Direction[] directions = new Direction[4] { Direction.West, Direction.North, Direction.East, Direction.South };

        public MazeDto Convert(MazeWithSolution maze)
        {
            byte[] structureAsBytes = CreateStructureByteArray(maze.Grid);
            byte[] solutionAsBytes = CreateSolutionByteArray(maze.SolutionPath);

            return new MazeDto(maze, structureAsBytes, solutionAsBytes);
        }

        private  byte[] CreateStructureByteArray(Maze maze)
        {
            byte[] structureAsBytes = new byte[maze.CellAmount];

            foreach (var (cell, index) in maze.WithIndex())
            {
                structureAsBytes[index] = ConvertCellToByte(cell);
            }

            return structureAsBytes;
        }

        private byte ConvertCellToByte(Cell cell)
        {
            byte cellAsByte = 0b0000_0000;
            cellAsByte += GetEnumValueIfLinkExists(cell, Direction.East);
            cellAsByte += GetEnumValueIfLinkExists(cell, Direction.South);
            return cellAsByte;
        }

        private byte GetEnumValueIfLinkExists(Cell cell, Direction direction)
        {
            return cell.IsLinkedToNeighbour(direction) ? (byte)direction : (byte)0;
        }

        private byte[] CreateSolutionByteArray(List<Cell> solutionPath)
        {
            byte[] solutionAsBytes = new byte[solutionPath.Count()];

            for(int i = 0; i < solutionPath.Count() - 1; i++)
            {
                var currentCell = solutionPath[i];
                var nextCell = solutionPath[i + 1];
                solutionAsBytes[i] = (byte)currentCell.GetNeighbourDirection(nextCell);
            }

            return solutionAsBytes;
        }
    }
}
