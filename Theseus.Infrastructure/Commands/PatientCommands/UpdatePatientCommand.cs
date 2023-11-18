using AutoMapper;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    public class UpdatePatientCommand : PatientCommand, IUpdatePatientCommand
    {
        public UpdatePatientCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Update(Patient patient)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                PatientDto patientDto = new PatientDto();
                Mapper.Map(patient, patientDto);
                AttachRelatedEntities(patientDto, context);
                context.Patients.Update(patientDto);
                await context.SaveChangesAsync();
            }
        }
    }
}