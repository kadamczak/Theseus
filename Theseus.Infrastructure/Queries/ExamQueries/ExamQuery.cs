using AutoMapper;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamQueries
{
    public abstract class ExamQuery : Query
    {
        protected ExamQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<Exam> MapExams(IEnumerable<ExamDto> examDtos)
        {
            List<Exam> exams = new List<Exam>();

            if (examDtos is null)
                return exams;

            foreach(var examDto in examDtos)
            {
                exams.Add(MapExam(examDto));
            }

            return exams;
        }

        protected Exam MapExam(ExamDto examDto)
        {
            Exam exam = new Exam();
            Mapper.Map(examDto, exam);
            return exam;
        }
    }
}