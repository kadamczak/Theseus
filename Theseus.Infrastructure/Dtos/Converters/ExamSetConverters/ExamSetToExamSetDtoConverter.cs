using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;

namespace Theseus.Infrastructure.Dtos.Converters.ExamSetConverters
{
    public class ExamSetToExamSetDtoConverter
    {
        private readonly MazeWithSolutionToMazeDtoConverter _toMazeDto;

        public ExamSetToExamSetDtoConverter(MazeWithSolutionToMazeDtoConverter toMazeDto)
        {
            _toMazeDto = toMazeDto;
        }

        public ExamSetDto Convert(ExamSet examSet)
        {
            List<MazeDto> mazeDtos = new List<MazeDto>();

            foreach (var maze in examSet.MazesWithSolution)
            {
                //MazeDto mazeDto = _toMazeDto.Convert(maze);
                //mazeDtos.Add(mazeDto);
            }

            return new ExamSetDto(examSet.Id, mazeDtos);
        }

        public ExamSetDto ConvertUsingAttach(ExamSet examSet, TheseusDbContext context)
        {
            List<MazeDto> mazeDtos = new List<MazeDto>();

            foreach (var maze in examSet.MazesWithSolution)
            {
                MazeDto mazeDto = new MazeDto(maze.Id);
                context.Mazes.Attach(mazeDto);
                mazeDtos.Add(mazeDto);
            }

            return new ExamSetDto(examSet.Id, mazeDtos);
        }
    }
}