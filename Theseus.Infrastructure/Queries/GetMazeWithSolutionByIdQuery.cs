using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters;

namespace Theseus.Infrastructure.Queries
{
    public class GetMazeWithSolutionByIdQuery : IGetMazeWithSolutionByIdQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly MazeDtoToMazeWithSolutionConverter _toMazeWithSolutionConverter;

        public GetMazeWithSolutionByIdQuery(TheseusDbContextFactory dbContextFactory, MazeDtoToMazeWithSolutionConverter toMazeWithSolutionConverter)
        {
            this._dbContextFactory = dbContextFactory;
            this._toMazeWithSolutionConverter = toMazeWithSolutionConverter;
        }

        public async Task<MazeWithSolution> GetMazeWithSolutionById(Guid id)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                MazeDto mazeEntity = await context.Mazes.FindAsync(id) ?? throw new KeyNotFoundException($"Maze entity with the id {id} has not been found in the database.");
                return this._toMazeWithSolutionConverter.Convert(mazeEntity);
            }
        }
    }
}