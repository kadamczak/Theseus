using AutoMapper;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamSetCommands
{
    public class RemoveExamSetCommand : ExamSetCommand, IRemoveExamSetCommand
    {
        public RemoveExamSetCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Remove(Guid examSetId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                ExamSetDto? examSetDto = await context.ExamSets.FindAsync(examSetId);

                if (examSetDto is null)
                    return;

                context.ExamSets.Remove(examSetDto);
                await context.SaveChangesAsync();
            }
        }
    }
}