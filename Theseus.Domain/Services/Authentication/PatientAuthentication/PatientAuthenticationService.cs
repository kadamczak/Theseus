using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.GroupRelated.Exceptions;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;

namespace Theseus.Domain.Services.Authentication.PatientAuthentication
{
    /// <summary>
    /// The <c>PatientAuthenticationService</c> class implements <c>Patient</c> login and registration.
    /// </summary>
    public class PatientAuthenticationService : IPatientAuthenticationService
    {
        private readonly IGetPatientByUsernameQuery _getPatientByUsernameQuery;
        private readonly IGetGroupByNameQuery _getGroupByNameQuery;
        private readonly IGetGroupByPatientQuery _getGroupByPatientQuery;
        private readonly ICreatePatientCommand _createPatientCommand;

        public PatientAuthenticationService(IGetPatientByUsernameQuery getPatientByUsernameQuery,
                                            IGetGroupByNameQuery getGroupByNameQuery,
                                            IGetGroupByPatientQuery getGroupByPatientQuery,
                                            ICreatePatientCommand createPatientCommand)
        {
            _getPatientByUsernameQuery = getPatientByUsernameQuery;
            _getGroupByNameQuery = getGroupByNameQuery;
            _getGroupByPatientQuery = getGroupByPatientQuery;
            _createPatientCommand = createPatientCommand;
        }

        public async Task<Patient> Login(string username, string groupName)
        {
            Patient? existingPatient = await _getPatientByUsernameQuery.GetPatient(username) ?? throw new UserNotFoundException(username);

            Group? group = _getGroupByPatientQuery.GetGroup(existingPatient.Id);
            if (group is null || group.Name != groupName)
            {
                throw new WrongGroupNameForPatientException(groupName, existingPatient.Username);
            }

            return existingPatient;
        }

        public async Task<PatientRegistrationResult> Register(Patient newAccount, string groupName)
        {
            PatientRegistrationResult result = PatientRegistrationResult.Success;

            try
            {
                Patient? patientWithSameUsername = await _getPatientByUsernameQuery.GetPatient(newAccount.Username);
                if (patientWithSameUsername is not null)
                {
                    result = PatientRegistrationResult.UsernameAlreadyExists;
                }

                Group? group = await _getGroupByNameQuery.GetGroup(groupName);
                if (group is null)
                {
                    result = PatientRegistrationResult.GroupDoesNotExist;
                }

                if (result == PatientRegistrationResult.Success)
                {
                    newAccount.Group = group!;
                    await _createPatientCommand.Create(newAccount);
                }
            }
            catch(Exception)
            {
                return PatientRegistrationResult.ConnectionFailed;
            }

            return result;
        }
    }
}