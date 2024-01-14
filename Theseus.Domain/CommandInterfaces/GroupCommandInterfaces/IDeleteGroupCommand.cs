namespace Theseus.Domain.CommandInterfaces.GroupCommandInterfaces
{
    /// <summary>
    /// Interface defining <c>Group</c> deletion method.
    /// </summary>
    public interface IDeleteGroupCommand
    {
        Task Delete(Guid groupId);
    }
}