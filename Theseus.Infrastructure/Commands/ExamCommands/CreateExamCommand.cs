using AutoMapper;
using Theseus.Domain.CommandInterfaces.ExamCommandInterfaces;
using Theseus.Domain.Models.ExamRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamCommands
{
    public class CreateExamCommand : Command, ICreateExamCommand
    {
        public CreateExamCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Create(Exam exam)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                ExamDto examDto = new ExamDto();
                Mapper.Map(exam, examDto);

                context.Attach(examDto.Patient);
                context.Attach(examDto.ExamSetDto);

                context.Exams.Add(examDto);
                await context.SaveChangesAsync();
            }
        }
    }
}