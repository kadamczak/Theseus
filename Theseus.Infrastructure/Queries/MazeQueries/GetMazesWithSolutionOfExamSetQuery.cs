using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetMazesWithSolutionOfExamSetQuery : MazeQuery, IGetMazesWithSolutionOfExamSetQuery
    {
        public GetMazesWithSolutionOfExamSetQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid examSetId)
        {
            
        }
    }
}