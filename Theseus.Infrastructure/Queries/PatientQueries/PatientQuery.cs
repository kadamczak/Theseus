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

        protected List<Patient> GetPatients(TheseusDbContext context, IEnumerable<PatientDto> patientDtos, bool loadGroup)
        {
            List<Patient> patients = new List<Patient>();
            foreach (var patient in patientDtos)
            {
                patients.Add(GetPatient(context, patient, loadGroup));
            }
            return patients;
        }

        protected Patient GetPatient(TheseusDbContext context, PatientDto patientDto, bool loadGroup)
        {
            //if (loadGroup)
            //    context.Entry(patientDto).Reference(p => p.GroupDto).Load();

            Patient patient = MapToPatient(patientDto);
            context.Entry(patientDto).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return patient;
        }

        private Patient MapToPatient(PatientDto patientDto)
        {
            Patient patient = new Patient();
            Mapper.Map(patientDto, patient);
            return patient;
        }
    }
}