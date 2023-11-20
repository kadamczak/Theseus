using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    public class GetMazesWithSolutionOfExamSetQuery : MazeQuery, IGetMazesWithSolutionOfExamSetQuery
    {
        public GetMazesWithSolutionOfExamSetQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid examSetId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeDtos = context.Mazes.AsNoTracking().Where(m => m.ExamSetDtos.Where(e => e.Id == examSetId).Any());
                return MapMazesWithSolution(mazeDtos);
            }
        }
    }
}