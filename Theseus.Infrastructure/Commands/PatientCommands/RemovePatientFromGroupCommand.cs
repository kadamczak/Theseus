using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    public class RemovePatientFromGroupCommand : PatientCommand, IRemovePatientFromGroupCommand
    {
        public RemovePatientFromGroupCommand(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        public async Task RemoveFromGroup(Patient patient)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                PatientDto patientDto = context.Patients.Include(p => p.GroupDto).First(p => p.Id == patient.Id);
                patientDto.GroupDto = null;

                context.Patients.Update(patientDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
