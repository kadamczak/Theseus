namespace Theseus.Domain.CommandInterfaces.GroupCommandInterfaces
{
    public interface IDeleteGroupCommand
    {
        Task Delete(Guid groupId);
    }
}