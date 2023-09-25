using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theseus.Code.DbContexts;
using Theseus.Code.MVVM.Models.Maze.Converters;
using Theseus.Code.MVVM.Models.Maze.Dto;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.Services.Database.MazeGridProviders
{
    public class DatabaseMazeGridProvider : IMazeGridProvider
    {
        private readonly TheseusDbContextFactory _dbContextFactory;

        public DatabaseMazeGridProvider(TheseusDbContextFactory dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<MazeGrid>> GetAllMazeGrids()
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeEntities = await context.Mazes.ToListAsync();
                return mazeEntities.Select(m => MazeDtoToMazeGridConverter.Convert(m)); 
            }
        }
    }
}
