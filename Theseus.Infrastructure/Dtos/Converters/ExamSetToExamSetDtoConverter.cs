using Theseus.Domain.Models.SetRelated;
using Theseus.Infrastructure.DbContexts;

namespace Theseus.Infrastructure.Dtos.Converters
{
    public class ExamSetToExamSetDtoConverter
    {
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