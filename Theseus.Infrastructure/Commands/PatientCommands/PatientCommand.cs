using AutoMapper;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    /// <summary>
    /// Abstract class defining attachment of related entities to the <c>PatientDto</c>.
    /// </summary>
    public abstract class PatientCommand : Command
    {
        protected PatientCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected void AttachRelatedEntities(PatientDto patientDto, TheseusDbContext context)
        {
            if (patientDto.GroupDto is not null)
                context.Attach(patientDto.GroupDto);
        }
    }
}
