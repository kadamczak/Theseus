using AutoMapper;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    /// <summary>
    /// Class implementing <c>Patient</c> addition to <c>Group</c>,
    /// using Entity Framework and <c>TheseusDbContextFactory</c>.
    /// All objects linked by foreign key need to already exist in database.
    /// </summary>
    public class AddPatientToGroupCommand : PatientCommand, IAddPatientToGroupCommand
    {
        public AddPatientToGroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task AddToGroup(Patient patient, Guid groupId)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                PatientDto patientDto = new PatientDto();
                Mapper.Map(patient, patientDto);

                patientDto.GroupDto = new GroupDto(groupId);
                AttachRelatedEntities(patientDto, context);
                context.Patients.Update(patientDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
