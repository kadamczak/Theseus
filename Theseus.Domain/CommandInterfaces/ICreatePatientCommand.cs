using Theseus.Domain.Models.UserRelated;

namespace Theseus.Domain.CommandInterfaces
{
    public interface ICreatePatientCommand
    {
        Task Create(Patient patient);
    }
}