using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Domain.QueryInterfaces.ExamQueryInterfaces;
using Theseus.Infrastructure.DbContexts;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public class GetExamStageOfExamSetOfIndexQuery : ExamStageQuery, IGetExamStagesOfExamSetOfIndexQuery
    {
        public GetExamStageOfExamSetOfIndexQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public IEnumerable<ExamStage> GetExamStages(Guid examSetId, int index)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                var examStages = context.ExamStages
                                        .AsNoTracking()
                                        .Where(e => e.Index == index)
                                        .Where(e => e.ExamDto.ExamSetDto.Id == examSetId)
                                        .Include(e => e.StepDtos)
                                        .Include(e => e.ExamDto)
                                        .ThenInclude(e => e.PatientDto)
                                        .Include(e => e.ExamDto)
                                        .ThenInclude(e => e.ExamSetDto);

                return MapExamStages(examStages);
            }
        }
    }
}