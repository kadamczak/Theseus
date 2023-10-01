using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.MazeRelated.MazeStructure;
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

        public async Task<IEnumerable<Maze>> GetAllMazes()
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeEntities = await context.Mazes.ToListAsync();
                return mazeEntities.Select(m => MazeDtoToMazeConverter.Convert(m));
            }
        }
    }
}
