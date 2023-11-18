using AutoMapper;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public abstract class MazeQuery : Query
    {
        protected MazeQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<MazeWithSolution> MapMazesWithSolution(IEnumerable<MazeDto> mazeDtos)
        {
            List<MazeWithSolution> mazes = new List<MazeWithSolution>();
            foreach (var mazeDto in mazeDtos)
            {
                mazes.Add(MapMazeWithSolution(mazeDto));
            }
            return mazes;
        }

        protected MazeWithSolution MapMazeWithSolution(MazeDto mazeDto)
        {
            //if (mazeDto.ExamSetDtos is null)
            //    return mazeWithSolution;

            //foreach (var examSetDto in mazeDto.ExamSetDtos)
            //{
            //    ExamSet examSet = new ExamSet();
            //    Mapper.Map(examSetDto, examSet);
            //    mazeWithSolution.ExamSets.Add(examSet);
            //}

            return Mapper.Map<MazeWithSolution>(mazeDto);
        }
    }
}