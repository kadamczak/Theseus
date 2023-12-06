using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public class GetExamsOfStaffMemberQuery : ExamQuery, IGetExamsOfStaffMemberQuery
    {
        public GetExamsOfStaffMemberQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<Exam> GetExams(Guid staffMemberId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamDto> examDtos = context.Exams
                                                       .Include(e => e.ExamSetDto)
                                                       .Include(e => e.PatientDto)
                                                       .Where(e => e.ExamSetDto.GroupDtos.Where(g => g.StaffMemberDtos.Where(s => s.Id == staffMemberId).Any()).Any())
                                                       .OrderByDescending(e => e.CreatedAt)
                                                       .AsNoTracking();
                return MapExams(examDtos);
            }
        }
    }
}