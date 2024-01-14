using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.CommandInterfaces.ExamSetCommandInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamSetCommands
{
    /// <summary>
    /// Class implementing <c>ExamSet</c> deletion method,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class DeleteExamSetCommand : ExamSetCommand, IDeleteExamSetCommand
    {
        public DeleteExamSetCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Delete(Guid examSetId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                ExamSetDto? examSetDto = await context.ExamSets.Include(e => e.ExamSetDto_MazeDto).Include(e => e.GroupDtos).Where(e => e.Id == examSetId).FirstAsync();

                if (examSetDto is null)
                    return;

                examSetDto.ExamSetDto_MazeDto.Clear();
                examSetDto.GroupDtos.Clear();
                context.ExamSets.Remove(examSetDto);
                await context.SaveChangesAsync();
            }
        }
    }
}