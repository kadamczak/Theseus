using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters;

namespace Theseus.Infrastructure.Queries
{
    public class GetAllMazesQuery : IGetAllMazesQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;

        public GetAllMazesQuery(TheseusDbContextFactory dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<MazeWithSolution>> GetAllMazes()
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeEntities = await context.Mazes.ToListAsync();
                return mazeEntities.Select(m => MazeDtoToMazeWithSolutionConverter.Convert(m));
            }
        }
    }
}
