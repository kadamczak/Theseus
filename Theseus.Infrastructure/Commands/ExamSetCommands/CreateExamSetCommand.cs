using AutoMapper;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamSetCommands
{
    public class CreateExamSetCommand : ExamSetCommand, ICreateExamSetCommand
    {
        public CreateExamSetCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task CreateExamSet(ExamSet examSet)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                ExamSetDto examSetDto = new ExamSetDto();
                Mapper.Map(examSet, examSetDto);
                AttachRelatedEntities(examSetDto, context);

                context.ExamSetDtos_MazeDtos.AddRange(examSetDto.ExamSetDto_MazeDto);
                context.ExamSets.Add(examSetDto);
                await context.SaveChangesAsync();
            }
        }
    }
}