using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.PatientCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>Patient</c> creation.
    /// </summary>
    public interface ICreatePatientCommand
    {
        Task Create(Patient patient);
    }
}