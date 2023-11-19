using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetMazesWithSolutionOfStaffMemberQuery : MazeQuery, IGetMazesWithSolutionOfStaffMemberQuery
    {
        public GetMazesWithSolutionOfStaffMemberQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper) { }

        public IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid staffMemberId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeDtos = context.Mazes.Where(m => m.Owner.Id == staffMemberId).AsNoTracking();
                return MapMazesWithSolution(mazeDtos);
            }
        }


    }
}