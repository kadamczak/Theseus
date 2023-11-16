namespace Theseus.Domain.Services.Authentication.StaffMemberAuthentication
{
    public enum StaffMemberRegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists
    }
}
