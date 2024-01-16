namespace Theseus.Domain.Services.Authentication.PatientAuthentication
{
    /// <summary>
    /// Possible scenarios of <c>Patient</c> registration.
    /// </summary>
    public enum PatientRegistrationResult
    {
        Success,
        UsernameAlreadyExists,
        GroupDoesNotExist,
        ConnectionFailed,
        PatientDataNotValid
    }
}