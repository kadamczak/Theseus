using AutoMapper;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    public class CreatePatientCommand : PatientCommand, ICreatePatientCommand
    {
        public CreatePatientCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task Create(Patient patient)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                PatientDto patientDto = new PatientDto();
                Mapper.Map(patient, patientDto);
                AttachRelatedEntities(patientDto, context);
                context.Patients.Add(patientDto);
                await context.SaveChangesAsync();
            }
        }
    }
}