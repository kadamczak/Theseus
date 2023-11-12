using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.PatientQueries
{
    public class GetPatientByUsernameQuery : IGetPatientByUsernameQuery
    {
        private readonly TheseusDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public GetPatientByUsernameQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<Patient?> GetPatient(string username)
        {
            using (TheseusDbContext context = _dbContextFactory.CreateDbContext())
            {
                PatientDto? patientDto = await context.Patients.FirstOrDefaultAsync(user => user.Username == username);
                return patientDto is null ? null : _mapper.Map<Patient>(patientDto);
            }
        }
    }
}