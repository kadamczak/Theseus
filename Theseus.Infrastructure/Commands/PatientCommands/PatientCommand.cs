using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    public abstract class PatientCommand : Command
    {
        protected PatientCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(PatientDto patientDto, TheseusDbContext context)
        {
            context.Attach(patientDto.GroupDto);
        }
    }
}
