namespace Theseus.Domain.Models.UserRelated.Exceptions
{
    /// <summary>
    /// The <c>InvalidPasswordException</c> class represents an exception that occurs when an user enters a password that does not match the specified <c>StaffMember</c>'s hashed password.
    /// </summary>
    public class InvalidPasswordException : Exception
    {
        /// <summary>
        /// Gets or sets the name of the <c>StaffMember</c> account.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the incorrect password entered by the program's user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Initializes <c>InvalidPasswordException</c> with the <c>StaffMember</c> username and incorrect password entered by the program's user.
        /// </summary>
        /// <param name="username"><c>StaffMember</c> username.</param>
        /// <param name="password">Incorrect password entered by the program's user.</param>
        public InvalidPasswordException(string username, string password) : base($"Invalid password {password} entered for user {username}.")
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Initializes <c>InvalidPasswordException</c> with the <c>StaffMember</c> username, incorrect password entered by the program's user and a custom message.
        /// </summary>
        /// <param name="message">Custom message</param>
        /// <param name="username"><c>StaffMember</c> username.</param>
        /// <param name="password">Incorrect password entered by the program's user.</param>
        public InvalidPasswordException(string message, string username, string password) : base(message)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Initializes <c>InvalidPasswordException</c> with the <c>StaffMember</c> username, incorrect password entered by the program's user, a custom message and an inner exception.
        /// </summary>
        /// <param name="message">Custom message</param>
        /// <param name="innerException">Inner exception.</param>
        /// <param name="username"><c>StaffMember</c> username.</param>
        /// <param name="password">Incorrect password entered by the program's user.</param>
        public InvalidPasswordException(string message, Exception innerException, string username, string password) : base(message, innerException)
        {
            Username = username;
            Password = password;
        }
    }
}
