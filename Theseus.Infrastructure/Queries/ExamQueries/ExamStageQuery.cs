using AutoMapper;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    /// <summary>
    /// Abstract query class, with method for <c>ExamStageDto</c> to <c>ExamStage</c> mapping.
    /// </summary>
    public abstract class ExamStageQuery : Query
    {
        protected ExamStageQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<ExamStage> MapExamStages(IEnumerable<ExamStageDto> examStageDtos)
        {
            List<ExamStage> examStages = new List<ExamStage>();

            if (examStageDtos is null)
                return examStages;

            foreach (var examStageDto in examStageDtos)
            {
                examStages.Add(MapExamStage(examStageDto));
            }

            return examStages;
        }

        protected ExamStage MapExamStage(ExamStageDto examStageDto)
        {
            ExamStage examStage = new ExamStage();
            Mapper.Map(examStageDto, examStage);
            return examStage;
        }
    }
}