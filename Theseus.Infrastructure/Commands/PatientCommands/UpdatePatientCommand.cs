using AutoMapper;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    public class UpdatePatientCommand : IUpdatePatientCommand
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public UpdatePatientCommand(TheseusDbContextFactory theseusDbContextFactory, IMapper mapper)
        {
            _dbContextFactory = theseusDbContextFactory;
            _mapper = mapper;
        }

        public async Task Update(Patient patient)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                var patientDto = _mapper.Map<PatientDto>(patient);

                AttachRelatedEntities(patientDto, context);
                foreach (var staffMember in patientDto.StaffMemberDtos)
                {
                    context.Attach(staffMember);
                }

                context.Patients.Update(patientDto);
                await context.SaveChangesAsync();
            }
        }

        private void AttachRelatedEntities(PatientDto patientDto, TheseusDbContext context)
        {
            foreach (var staffMember in patientDto.StaffMemberDtos)
            {
                context.Attach(staffMember);
            }
        }
    }
}