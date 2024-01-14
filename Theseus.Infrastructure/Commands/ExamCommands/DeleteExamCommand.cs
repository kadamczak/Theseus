using AutoMapper;
using Theseus.Domain.CommandInterfaces.ExamCommandInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamCommands
{
    /// <summary>
    /// Class implementing <c>Exam</c> deletion method,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// </summary>
    public class DeleteExamCommand : Command, IDeleteExamCommand
    {
        public DeleteExamCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Delete(Guid examId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                ExamDto exam = await context.Exams.FindAsync(examId);
                context.Exams.Remove(exam);
                await context.SaveChangesAsync();
            }
        }
    }
}