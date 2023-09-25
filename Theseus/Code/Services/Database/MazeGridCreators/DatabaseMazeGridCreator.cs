using System.Threading.Tasks;
using Theseus.Code.DbContexts;
using Theseus.Code.MVVM.Models.Maze.Converters;
using Theseus.Code.MVVM.Models.Maze.Dto;
using Theseus.Code.MVVM.Models.Maze.GridStructure;

namespace Theseus.Code.Services.Database.MazeGridCreators
{
    public class DatabaseMazeGridCreator : IMazeGridCreator
    {
        private readonly TheseusDbContextFactory _dbContextFactory;

        public DatabaseMazeGridCreator(TheseusDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateMazeGrid(MazeGrid maze)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto mazeDto = MazeGridToMazeDtoConverter.Convert(maze);
                context.Mazes.Add(mazeDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
