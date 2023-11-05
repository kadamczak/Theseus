using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters;

namespace Theseus.Infrastructure.Queries
{
    public class GetAllMazesWithSolutionQuery : IGetAllMazesWithSolutionQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly MazeDtoToMazeWithSolutionConverter _toMazeWithSolutionConverter;

        public GetAllMazesWithSolutionQuery(TheseusDbContextFactory dbContextFactory, MazeDtoToMazeWithSolutionConverter toMazeWithSolutionConverter)
        {
            this._dbContextFactory = dbContextFactory;
            this._toMazeWithSolutionConverter = toMazeWithSolutionConverter;
        }

        public IEnumerable<MazeWithSolution> GetAllMazesWithSolution()
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeEntities = context.Mazes.ToList();
                return mazeEntities.Select(m => this._toMazeWithSolutionConverter.Convert(m));
            }
        }
    }
}