using AutoMapper;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.MazeQueries
{
    /// <summary>
    /// Class defining retrieval of <c>MazeWithSolution</c>s used in a specified <c>ExamSet</c> in the correct order,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class GetOrderedMazesWithSolutionOfExamSetQuery : MazeQuery, IGetOrderedMazesWithSolutionOfExamSetQuery
    {
        public GetOrderedMazesWithSolutionOfExamSetQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<MazeWithSolution> GetMazesWithSolution(Guid examSetId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<MazeDto> mazeDtos = context.ExamSetDtos_MazeDtos.Where(j => j.ExamSetDto.Id == examSetId).OrderBy(j => j.Index).Select(j => j.MazeDto);
                return MapMazesWithSolution(mazeDtos);
            }
        }
    }
}