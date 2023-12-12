namespace Theseus.Domain.Models.UserRelated.Exceptions
{
    /// <summary>
    /// The <c>UserNotFoundException</c> class represents an exception thrown when the user wants to access an account with the specified username but it does not exist.
    /// </summary>
    /// <remarks>
    /// This exception applies both to <c>Patient</c> accounts and <c>StaffMember</c> accounts.
    /// </remarks>
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Gets or sets the username entered by the application's user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Initializes <c>UserNotFoundException</c> with the username entered by the application's user.
        /// </summary>
        /// <param name="username">Username entered by the application's user.</param>
        public UserNotFoundException(string username) : base($"User {username} wasn't found in the database.")
        {
            Username = username;
        }

        /// <summary>
        /// Initializes <c>UserNotFoundException</c> with the username entered by the application's user and a custom message.
        /// </summary>
        /// <param name="message">Custom message.</param>
        /// <param name="username">Username entered by the application's user.</param>
        public UserNotFoundException(string message, string username) : base(message)
        {
            Username = username;
        }

        /// <summary>
        /// Initializes <c>UserNotFoundException</c> with the username entered by the application's user, a custom message and an inner exception.
        /// </summary>
        /// <param name="message">Custom message.</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="username">Username entered by the application's user.</param>
        public UserNotFoundException(string message, Exception innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}
