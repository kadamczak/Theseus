using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.PatientCommandInterfaces
{
    public interface IRemovePatientFromGroupCommand
    {
        Task RemoveFromGroup(Patient patient);
    }
}