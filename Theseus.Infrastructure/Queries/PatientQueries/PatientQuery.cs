using AutoMapper;
using Theseus.Domain.Models.UserRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.PatientQueries
{
    public abstract class PatientQuery : Query
    {
        protected PatientQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<Patient> MapPatients(IEnumerable<PatientDto> patientDtos)
        {
            List<Patient> patients = new List<Patient>();
            foreach (var patient in patientDtos)
            {
                patients.Add(MapPatient(patient));
            }
            return patients;
        }

        protected Patient MapPatient(PatientDto patientDto)
        {
            Patient patient = new Patient();
            Mapper.Map(patientDto, patient);
            return patient;
        }
    }
}