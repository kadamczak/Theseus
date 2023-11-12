using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetMazeWithSolutionByIdQuery : IGetMazeWithSolutionByIdQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly MazeDtoToMazeWithSolutionConverter _toMazeWithSolutionConverter;

        public GetMazeWithSolutionByIdQuery(TheseusDbContextFactory dbContextFactory, MazeDtoToMazeWithSolutionConverter toMazeWithSolutionConverter)
        {
            _dbContextFactory = dbContextFactory;
            _toMazeWithSolutionConverter = toMazeWithSolutionConverter;
        }

        public MazeWithSolution? GetMazeWithSolutionById(Guid id)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto? mazeEntity = context.Mazes.Find(id);
                return mazeEntity is null ? null : _toMazeWithSolutionConverter.Convert(mazeEntity);
            }
        }
    }
}