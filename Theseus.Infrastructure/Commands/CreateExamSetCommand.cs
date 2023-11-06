using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.SetRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;
using Theseus.Infrastructure.Dtos.Converters.ExamSetConverters;

namespace Theseus.Infrastructure.Commands
{
    public class CreateExamSetCommand : ICreateExamSetCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly ExamSetToExamSetDtoConverter _toExamSetDtoConverter;

        public CreateExamSetCommand(TheseusDbContextFactory dbContextFactory,
                                    ExamSetToExamSetDtoConverter toExamSetDtoConverter)
        {
            this._dbContextFactory = dbContextFactory;
            this._toExamSetDtoConverter = toExamSetDtoConverter;
        }

        public async Task CreateExamSet(ExamSet examSet)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                ExamSetDto examSetDto = _toExamSetDtoConverter.ConvertUsingAttach(examSet, context);

                context.ExamSets.Add(examSetDto);
                await context.SaveChangesAsync();
            }
        }
    }
}