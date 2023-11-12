using AutoMapper;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands
{
    public class CreatePatientCommand : ICreatePatientCommand
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
                context.Patients.Add(patientDto);
                await context.SaveChangesAsync();
            }
        }
    }
}