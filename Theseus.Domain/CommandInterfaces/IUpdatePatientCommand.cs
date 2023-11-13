using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces
{
    public interface IUpdatePatientCommand
    {
        Task Update(Patient patient);
    }
}
