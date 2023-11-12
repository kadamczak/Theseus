namespace Theseus.Domain.Services.Authentication
{
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists,
        StaffMemberDoesNotExist
    }
}
