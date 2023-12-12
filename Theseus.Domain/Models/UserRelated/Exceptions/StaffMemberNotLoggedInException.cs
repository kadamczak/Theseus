namespace Theseus.Domain.Models.UserRelated.Exceptions
{
    /// <summary>
    /// The <c>StaffMemberNotLoggedInException</c> class represents an exception thrown when the user accesses a fragment of the application
    /// that requires a logged-in <c>StaffMember</c> but this requirement is not met.
    /// </summary>
    public class StaffMemberNotLoggedInException : Exception
    {
        /// <summary>
        /// Initializes <c>StaffMemberNotLoggedInException</c> with the standard exception message.
        /// </summary>
        public StaffMemberNotLoggedInException() : base($"Staff member was expected to be logged in but is not.")
        {
        }
    }
}