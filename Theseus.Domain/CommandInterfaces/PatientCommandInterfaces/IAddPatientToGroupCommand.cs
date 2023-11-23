using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces.PatientCommandInterfaces
{
    public interface IAddPatientToGroupCommand
    {
        Task AddToGroup(Patient patient, Guid groupId);
    }
}