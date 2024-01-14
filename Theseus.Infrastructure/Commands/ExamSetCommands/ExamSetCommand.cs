using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamSetCommands
{
    /// <summary>
    /// Abstract class defining attachment of related entities to the <c>ExamSetDto</c>.
    /// </summary>
    public abstract class ExamSetCommand : Command
    {
        protected ExamSetCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(ExamSetDto examSetDto, TheseusDbContext context)
        {
            if (examSetDto.Owner is not null)
                context.Attach(examSetDto.Owner);

            if (examSetDto.ExamSetDto_MazeDto is not null)
                AttachExamSetDto_MazeDto(examSetDto, context);

            if (examSetDto.GroupDtos is not null)
                AttachGroupDtos(examSetDto, context);

            if (examSetDto.ExamDtos is not null)
                AttachExamDtos(examSetDto, context);
        }

        private void AttachExamSetDto_MazeDto(ExamSetDto examSetDto, TheseusDbContext context)
        {
            foreach (var maze in examSetDto.ExamSetDto_MazeDto!)
                context.Attach(maze);
        }

        private void AttachGroupDtos(ExamSetDto examSetDto, TheseusDbContext context)
        {
            foreach (var group in examSetDto.GroupDtos)
                context.Attach(group);
        }

        private void AttachExamDtos(ExamSetDto examSetDto, TheseusDbContext context)
        {
            foreach (var exam in examSetDto.ExamDtos)
                context.Attach(exam);
        }
    }
}
