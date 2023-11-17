using AutoMapper;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    public class CreatePatientCommand : PatientCommand, ICreatePatientCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public CreatePatientCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task Create(Patient patient)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                var patientDto = _mapper.Map<PatientDto>(patient);

                AttachRelatedEntities(patientDto, context);

                context.Patients.Add(patientDto);
                await context.SaveChangesAsync();
            }
        }
    }
}