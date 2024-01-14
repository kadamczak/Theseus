using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    /// <summary>
    /// Abstract query class, with method for <c>MazeDto</c> to <c>MazeWithSolution</c> mapping.
    /// </summary>
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
            return Mapper.Map<MazeWithSolution>(mazeDto);
        }
    }
}