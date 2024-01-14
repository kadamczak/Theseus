using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    /// <summary>
    /// Class defining retrieval of <c>Exam</c>s belonging to a specified <c>Group</c> and using a specified <c>ExamSet</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// Related entities are included.
    /// </summary>
    public class GetExamsOfGroupOfExamSetWithFullIncludeQuery : ExamQuery, IGetExamsOfGroupOfExamSetWithFullIncludeQuery
    {
        public GetExamsOfGroupOfExamSetWithFullIncludeQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<Exam> GetExams(Guid groupId, Guid examSetId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamDto> examDtos = context.Exams
                                                       .Include(e => e.PatientDto)
                                                       .Include(e => e.ExamSetDto)
                                                       .Include(e => e.StageDtos)
                                                       .ThenInclude(s => s.StepDtos)
                                                       .Where(e => e.ExamSetDto.Id == examSetId && e.PatientDto.GroupDto.Id == groupId)
                                                       .AsNoTracking();

                return MapExams(examDtos);
            }
        }
    }
}