﻿using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Commands.PatientCommands
{
    public abstract class PatientCommand
    {
        protected void AttachRelatedEntities(PatientDto patientDto, TheseusDbContext context)
        {
            if (patientDto.StaffMemberDtos is null)
                return;

            foreach (var staffMember in patientDto.StaffMemberDtos)
            {
                context.Attach(staffMember);
            }
        }
    }
}
