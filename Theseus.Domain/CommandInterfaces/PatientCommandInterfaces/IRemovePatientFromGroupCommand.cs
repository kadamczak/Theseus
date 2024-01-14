using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.PatientCommandInterfaces
{
    /// <summary>
    /// Interface defining method of <c>Patient</c> removal from <c>Group</c>.
    /// </summary>
    public interface IRemovePatientFromGroupCommand
    {
        Task RemoveFromGroup(Patient patient);
    }
}