using System.ComponentModel.DataAnnotations;

namespace Theseus.WPF.Code.Services
{
    /// <summary>
    /// The <c>EmailValidator</c> class verifies validity of strings as email addresses.
    /// </summary>
    public class EmailValidator
    {
        public bool IsValid(string email) => new EmailAddressAttribute().IsValid(email);
    }
}