using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.MazeConverters;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetAllMazesWithSolutionQuery : IGetAllMazesWithSolutionQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly MazeDtoToMazeWithSolutionConverter _toMazeWithSolutionConverter;

        public GetAllMazesWithSolutionQuery(TheseusDbContextFactory dbContextFactory, MazeDtoToMazeWithSolutionConverter toMazeWithSolutionConverter)
        {
            _dbContextFactory = dbContextFactory;
            _toMazeWithSolutionConverter = toMazeWithSolutionConverter;
        }

        public IEnumerable<MazeWithSolution> GetAllMazesWithSolution()
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeEntities = context.Mazes.ToList();
                return mazeEntities.Select(m => _toMazeWithSolutionConverter.Convert(m));
            }
        }
    }
}