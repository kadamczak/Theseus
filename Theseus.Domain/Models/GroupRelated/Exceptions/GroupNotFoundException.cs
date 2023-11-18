namespace Theseus.Domain.Models.GroupRelated.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public string GroupName { get; set; }

        public GroupNotFoundException(string group) : base($"Group {group} wasn't found in the database.")
        {
            GroupName = group;
        }

        public GroupNotFoundException(string message, string group) : base(message)
        {
            GroupName = group;
        }

        public GroupNotFoundException(string message, Exception innerException, string group) : base(message, innerException)
        {
            GroupName = group;
        }
    }
}