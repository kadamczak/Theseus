using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.Domain.Services.ExamDataServices
{
    /// <summary>
    /// The <c>TimedCell</c> class combines a <c>Cell</c> and a float value which tells how much time has passed between previous position in <c>Maze</c>
    /// and movement to this <c>Cell</c>.
    /// </summary>
    public class TimedCell
    {
        public float TimeBeforeMove { get; set; }
        public Cell Cell { get; set; }

        public TimedCell(float timeBeforeMove, Cell cell)
        {
            TimeBeforeMove = timeBeforeMove;
            Cell = cell;
        }
    }

    /// <summary>
    /// The <c>InputListToTimedCellPathConverter</c> class has the ability to convert a collection of <c>ExamStep</c>s, which can contain wall hits,
    /// into a straightforward collection of <c>Cell</c>s and time intervals between them.
    /// </summary>
    public class InputListToTimedCellPathConverter
    {
        public IEnumerable<TimedCell> ConvertInputListToTimedCellList(IEnumerable<ExamStep> inputs, MazeWithSolution maze)
        {
            Cell currentCell = maze.SolutionPath.First();
            List<TimedCell> patientInputPath = new List<TimedCell>() { new TimedCell(0, currentCell) };

            float currentTime = 0f;
            foreach (var input in inputs)
            {
                currentTime += input.TimeBeforeStep;

                if (currentCell.IsLinkedToNeighbour(input.StepTaken))
                {
                    Cell nextCell = currentCell.GetAdjecentCellSpace(input.StepTaken)!;
                    patientInputPath.Add(new TimedCell(currentTime, nextCell));

                    currentTime = 0f;
                    currentCell = nextCell;
                }
            }

            return patientInputPath.AsReadOnly();
        }
    }
}