﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.PatientQueries
{
    public class GetPatientByUsernameQuery : PatientQuery, IGetPatientByUsernameQuery
    {
        public GetPatientByUsernameQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper) { }

        public async Task<Patient?> GetPatient(string username)
        {
            using (TheseusDbContext context = DbContextFactory.CreateDbContext())
            {
                PatientDto? patientDto = await context.Patients.AsNoTracking().FirstOrDefaultAsync(user => user.Username == username);
                return patientDto is null ? null : MapPatient(patientDto);
            }
        }
    }
}