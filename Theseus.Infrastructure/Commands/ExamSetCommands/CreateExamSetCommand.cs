using AutoMapper;
using Theseus.Domain.ExamSetCommandInterfaces;
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
                ExamSetDto examSetDto = Mapper.Map<ExamSetDto>(examSet);
                AttachRelatedEntities(examSetDto, context);
                context.ExamSets.Add(examSetDto);
                await context.SaveChangesAsync();
            }
        }
    }
}