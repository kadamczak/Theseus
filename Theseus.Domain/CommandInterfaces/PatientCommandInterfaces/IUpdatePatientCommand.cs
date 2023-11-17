using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.PatientCommandInterfaces
{
    public interface IUpdatePatientCommand
    {
        Task Update(Patient patient);
    }
}
