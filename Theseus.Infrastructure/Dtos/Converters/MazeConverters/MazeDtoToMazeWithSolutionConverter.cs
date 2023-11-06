using Theseus.Domain.Extensions;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.Exceptions;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Infrastructure.Dtos.Converters.MazeConverters
{
    public class MazeDtoToMazeWithSolutionConverter
    {
        Direction[] directions = new Direction[2] { Direction.South, Direction.East };

        public MazeWithSolution Convert(MazeDto mazeDto)
        {
            Maze maze = CreateMaze(mazeDto);
            MazeWithSolution mazeWithSolution = CreateMazeWithSolution(mazeDto, maze);

            return mazeWithSolution;
        }

        private Maze CreateMaze(MazeDto mazeDto)
        {
            Maze mazeGrid = new Maze(mazeDto.Height, mazeDto.Width);

            foreach (var (cell, index) in mazeGrid.WithIndex())
            {
                byte cellValue = mazeDto.Structure[index];
                LinkToNeighboursWhoseBitIsSetToOne(cell, cellValue);
            }

            return mazeGrid;
        }

        private void LinkToNeighboursWhoseBitIsSetToOne(Cell cell, byte cellValue)
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

        private bool IsBitSetToOne(byte value, int position)
        {
            return (value & 1 << position) != 0;
        }

        private MazeWithSolution CreateMazeWithSolution(MazeDto mazeDto, Maze mazeGrid)
        {
            MazeWithSolution maze = new MazeWithSolution(mazeGrid, mazeDto.Id);

            maze.StartDirection = (Direction)mazeDto.StartDirection;
            maze.EndDirection = (Direction)mazeDto.EndDirection;

            (int, int) startCellCoordinates = (mazeDto.SolutionStartRow, mazeDto.SolutionStartColumn);
            maze.SolutionPath = CreateSolutionPath(mazeDto.Solution, startCellCoordinates, mazeGrid);

            return maze;
        }

        private List<Cell> CreateSolutionPath(byte[] solutionAsBytes, (int, int) startCoordinates, Maze mazeGrid)
        {
            Cell startCell = mazeGrid.GetCell(startCoordinates) ?? throw new CellDoesNotExistException(startCoordinates);
            List<Cell> solutionPath = new List<Cell>() { startCell };

            var currentCell = startCell;
            foreach (byte directionToNextCell in solutionAsBytes)
            {
                Direction direction = (Direction)directionToNextCell;
                Cell nextCell = currentCell.AdjecentCellSpaces[direction] ?? throw new CellDoesNotExistException(currentCell, direction);

                if (!currentCell.IsLinked(nextCell))
                    throw new SolutionCellsNotLinkedException(currentCell, nextCell);

                solutionPath.Add(nextCell);
                currentCell = nextCell;
            }

            return solutionPath;
        }
    }
}
