using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    /// <summary>
    /// Interface defining retrieval of <c>Exam</c>s done by specified <c>Patient</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// Related entities are included.
    /// </summary>
    public class GetExamsOfPatientWithFullIncludeQuery : ExamQuery, IGetExamsOfPatientWithFullIncludeQuery
    {
        public GetExamsOfPatientWithFullIncludeQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<Exam> GetExams(Guid patientId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamDto> examDtos = context.Exams
                                                       .Include(e => e.ExamSetDto)
                                                       .Include(e => e.PatientDto)
                                                       .ThenInclude(p => p.GroupDto)
                                                       .Include(e => e.StageDtos)
                                                       .ThenInclude(e => e.StepDtos)
                                                       .Where(e => e.PatientDto.Id == patientId)
                                                       .OrderByDescending(e => e.CreatedAt)
                                                       .AsNoTracking();

                return MapExams(examDtos);
            }
        }
    }
}