namespace Theseus.Domain.Models.UserRelated.Exceptions
{
    public class StaffMemberNotLoggedInException : Exception
    {
        public StaffMemberNotLoggedInException() : base($"Staff member was expected to be logged in but is not.")
        {
        }
    }
}
