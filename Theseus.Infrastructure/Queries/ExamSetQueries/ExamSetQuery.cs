using AutoMapper;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamSetQueries
{
    public abstract class ExamSetQuery : Query
    {
        protected ExamSetQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<ExamSet> GetExamSets(TheseusDbContext context, IEnumerable<ExamSetDto> examSetDtos, bool loadOwner, bool loadMazes, bool loadGroups)
        {
            List<ExamSet> examSets = new List<ExamSet>();
            foreach (var examSet in examSetDtos)
            {
                examSets.Add(GetExamSet(context, examSet, loadOwner, loadMazes, loadGroups));
            }
            return examSets;
        }

        protected ExamSet GetExamSet(TheseusDbContext context, ExamSetDto examSetDto, bool loadOwner, bool loadMazes, bool loadGroups)
        {
            if (loadOwner)
                context.Entry(examSetDto).Reference(p => p.Owner).Load();

            if (loadMazes)
                context.Entry(examSetDto).Collection(p => p.MazeDtos).Load();

            if (loadGroups)
                context.Entry(examSetDto).Collection(p => p.GroupDtos).Load();

            return MapToExamSet(examSetDto);
        }

        private ExamSet MapToExamSet(ExamSetDto groupDto) => Mapper.Map<ExamSet>(groupDto);
    }
}