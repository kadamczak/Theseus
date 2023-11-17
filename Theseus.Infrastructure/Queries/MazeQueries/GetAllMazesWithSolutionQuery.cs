using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetAllMazesWithSolutionQuery : MazeQuery, IGetAllMazesWithSolutionQuery
    {
        public GetAllMazesWithSolutionQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper) { }

        public IEnumerable<MazeWithSolution> GetAllMazesWithSolution(bool loadOwner = false, bool loadExamSets = false)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeDtos = context.Mazes.ToList();
                return GetMazesWithSolution(context, mazeDtos, loadOwner, loadExamSets);
            }
        }
    }
}