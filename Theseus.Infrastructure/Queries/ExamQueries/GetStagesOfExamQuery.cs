using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public class GetStagesOfExamQuery : ExamStageQuery, IGetStagesOfExamQuery
    {
        public GetStagesOfExamQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<ExamStage> GetStages(Guid examId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                IEnumerable<ExamStageDto> examStageDtos = context.ExamStages.Where(s => s.ExamDto.Id == examId).AsNoTracking();
                return MapExamStages(examStageDtos);
            }
        }
    }
}