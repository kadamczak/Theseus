using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetMazeWithSolutionByIdQuery : MazeQuery, IGetMazeWithSolutionByIdQuery
    {
        public GetMazeWithSolutionByIdQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper) { }

        public MazeWithSolution? GetMazeWithSolutionById(Guid id)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                MazeDto? mazeDto = context.Mazes.Find(id);
                return (mazeDto is null) ? null : MapMazeWithSolution(mazeDto);
            }
        }
    }
}