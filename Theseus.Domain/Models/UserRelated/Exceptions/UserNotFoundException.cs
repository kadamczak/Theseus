namespace Theseus.Domain.Models.UserRelated.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string Username { get; set; }

        public UserNotFoundException(string username) : base($"User {username} wasn't found in the database.")
        {
            Username = username;
        }

        public UserNotFoundException(string message, string username) : base(message)
        {
            Username = username;
        }

        public UserNotFoundException(string message, Exception innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}
