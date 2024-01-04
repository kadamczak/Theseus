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
                StaffMemberDto staffMemberDto = context.StaffMembers.Include(s => s.GroupDtos).First(s => s.Id == staffMemberId);


                IEnumerable<ExamDto> examDtos = context.Exams
                                                       .Include(e => e.ExamSetDto)
                                                       .Include(e => e.PatientDto)
                                                       .ThenInclude(p => p.GroupDto)
                                                       .Include(e => e.StageDtos)
                                                       .ThenInclude(e => e.StepDtos)
                                                       .Where(e => staffMemberDto.GroupDtos.Contains(e.PatientDto.GroupDto))
                                                       //.Where(e => e.ExamSetDto.GroupDtos.Where(g => g.StaffMemberDtos.Where(s => s.Id == staffMemberId).Any()).Any())
                                                       .OrderByDescending(e => e.CreatedAt)
                                                       .AsNoTracking();
                return MapExams(examDtos);
            }
        }
    }
}