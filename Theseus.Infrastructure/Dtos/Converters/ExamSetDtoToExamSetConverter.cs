using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.SetRelated;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class ExamSetDtoToExamSetConverter
    {
        private readonly MazeDtoToMazeWithSolutionConverter _toMazeWithSolution;

        public ExamSetDtoToExamSetConverter(MazeDtoToMazeWithSolutionConverter toMazeWithSolution)
        {
            this._toMazeWithSolution = toMazeWithSolution;
        }

        public ExamSet Convert(ExamSetDto examSetDto)
        {
            List<MazeWithSolution> mazesWithSolution = new List<MazeWithSolution>();

            foreach (var mazeDto in examSetDto.MazeDtos)
            {
                MazeWithSolution maze = _toMazeWithSolution.Convert(mazeDto);
                mazesWithSolution.Add(maze);
            }

            return new ExamSet(examSetDto.Id, mazesWithSolution);
        }
    }
}
