using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.PatientCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>Patient</c> addition to <c>Group</c>.
    /// </summary>
    public interface IAddPatientToGroupCommand
    {
        Task AddToGroup(Patient patient, Guid groupId);
    }
}