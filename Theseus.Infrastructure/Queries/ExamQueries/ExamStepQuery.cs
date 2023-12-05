using AutoMapper;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public abstract class ExamStepQuery : Query
    {
        public ExamStepQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<ExamStep> MapExamSteps(IEnumerable<ExamStepDto> examStepDtos)
        {
            List<ExamStep> examSteps = new List<ExamStep>();

            if (examStepDtos is null)
                return examSteps;

            foreach (var examStepDto in examStepDtos)
            {
                examSteps.Add(MapExamStep(examStepDto));
            }

            return examSteps;
        }

        protected ExamStep MapExamStep(ExamStepDto examStepDto)
        {
            ExamStep examStep = new ExamStep();
            Mapper.Map(examStepDto, examStep);
            return examStep;
        }
    }
}