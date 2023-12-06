using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public class GetExamsOfGroupOfExamSetQuery : ExamQuery, IGetExamsOfGroupOfExamSetQuery
    {
        public GetExamsOfGroupOfExamSetQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<Exam> GetExams(Guid groupId, Guid examSetId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamDto> examDtos = context.Exams
                                                       .Include(e => e.PatientDto)
                                                       .Include(e => e.StageDtos)
                                                       .ThenInclude(s => s.StepDtos)
                                                       .Where(e => e.ExamSetDto.Id == examSetId && e.PatientDto.GroupDto.Id == groupId)
                                                       .AsNoTracking();

                return MapExams(examDtos);
            }
        }
    }
}