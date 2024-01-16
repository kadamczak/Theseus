namespace Theseus.Domain.Services.Authentication.StaffMemberAuthentication
{
    /// <summary>
    /// Possible scenarios of <c>StaffMember</c> registration.
    /// </summary>
    public enum StaffMemberRegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists,
        ConnectionFailed,
        StaffMemberDataNotValid
    }
}
