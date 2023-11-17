using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.PatientCommandInterfaces
{
    public interface ICreatePatientCommand
    {
        Task Create(Patient patient);
    }
}