using AutoMapper;
using Theseus.Domain.ExamSetCommandInterfaces;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.ExamSetCommands
{
    public class CreateExamSetCommand : ExamSetCommand, ICreateExamSetCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public CreateExamSetCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task CreateExamSet(ExamSet examSet)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                ExamSetDto examSetDto = _mapper.Map<ExamSetDto>(examSet);

                AttachRelatedEntities(examSetDto, context);

                context.ExamSets.Add(examSetDto);
                await context.SaveChangesAsync();
            }
        }
    }
}