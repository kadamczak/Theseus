using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public class GetExamsOfPatientQuery : ExamQuery, IGetExamsOfPatientQuery
    {
        public GetExamsOfPatientQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
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
                                                       .Where(e => e.PatientDto.Id == patientId)
                                                       .OrderByDescending(e => e.CreatedAt)
                                                       .AsNoTracking();

                return MapExams(examDtos);
            }
        }
    }
}