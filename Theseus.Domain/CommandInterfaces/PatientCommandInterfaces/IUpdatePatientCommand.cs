using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.PatientCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>Patient</c> data update.
    /// </summary>
    public interface IUpdatePatientCommand
    {
        Task Update(Patient patient);
    }
}
