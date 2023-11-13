using System.ComponentModel.DataAnnotations;

namespace Theseus.WPF.Code.Services
{
    public class EmailValidator : IEmailValidator
    {
        public bool IsValid(string email) => new EmailAddressAttribute().IsValid(email);
    }
}