using System.Collections.Generic;
using System.Linq;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.Services
{
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

    public class InputListToTimedCellPathConverter
    {
        public List<TimedCell> ConvertInputListToTimedCellList(IEnumerable<ExamStep> inputs, MazeWithSolution maze)
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

            return patientInputPath;
        }
    }
}