using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Domain.Services.Authentication.StaffMemberAuthentication;

namespace Theseus.Domain.Services.Authentication.PatientAuthentication
{
    public class PatientAuthenticationService : IPatientAuthenticationService
    {
        private readonly IGetPatientByUsernameQuery _getPatientByUsernameQuery;
        private readonly IGetStaffMemberByUsernameQuery _getStaffMemberByUsernameQuery;
        private readonly ICreatePatientCommand _createPatientCommand;

        public PatientAuthenticationService(IGetPatientByUsernameQuery getPatientByUsernameQuery,
                                            IGetStaffMemberByUsernameQuery getStaffMemberByUsernameQuery,
                                            ICreatePatientCommand createPatientCommand)
        {
            _getPatientByUsernameQuery = getPatientByUsernameQuery;
            _getStaffMemberByUsernameQuery = getStaffMemberByUsernameQuery;
            _createPatientCommand = createPatientCommand;
        }

        public async Task<Patient> Login(string username)
        {
            Patient? existingPatient = await _getPatientByUsernameQuery.GetPatient(username);

            if (existingPatient is null)
            {
                throw new UserNotFoundException(username);
            }

            return existingPatient;
        }

        public async Task<PatientRegistrationResult> Register(Patient newAccount, string staffMemberUsername)
        {
            PatientRegistrationResult result = PatientRegistrationResult.Success;

            Patient? patientWithSameUsername = await _getPatientByUsernameQuery.GetPatient(newAccount.Username);
            if (patientWithSameUsername is not null)
            {
                result = PatientRegistrationResult.UsernameAlreadyExists;
            }

            StaffMember? staffMember = await _getStaffMemberByUsernameQuery.GetStaffMember(staffMemberUsername);
            if (staffMember is null)
            {
                result = PatientRegistrationResult.StaffMemberDoesNotExist;
            }

            if (result == PatientRegistrationResult.Success)
            {
                newAccount.StaffMembers.Add(staffMember!);
                await _createPatientCommand.Create(newAccount);
            }

            return result;
        }
    }
}