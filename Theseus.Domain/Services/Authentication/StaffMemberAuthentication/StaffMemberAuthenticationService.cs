using Microsoft.AspNet.Identity;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;

namespace Theseus.Domain.Services.Authentication.StaffMemberAuthentication
{
    public class StaffMemberAuthenticationService : IStaffMemberAuthenticationService
    {
        private readonly IGetStaffMemberByUsernameQuery _getStaffMemberByUsernameQuery;
        private readonly IGetStaffMemberByEmailQuery _getStaffMemberByEmailQuery;
        private readonly ICreateStaffMemberCommand _createStaffMemberCommand;
        private readonly IPasswordHasher _passwordHasher;

        public StaffMemberAuthenticationService(IGetStaffMemberByUsernameQuery getStaffMemberByUsernameQuery,
                                     IGetStaffMemberByEmailQuery getStaffMemberByEmailQuery,
                                     ICreateStaffMemberCommand createStaffMemberCommand,
                                     IPasswordHasher passwordHasher)
        {
            _getStaffMemberByUsernameQuery = getStaffMemberByUsernameQuery;
            _getStaffMemberByEmailQuery = getStaffMemberByEmailQuery;
            _createStaffMemberCommand = createStaffMemberCommand;
            _passwordHasher = passwordHasher;
        }

        public async Task<StaffMember> Login(string username, string password)
        {
            StaffMember? existingAccount = await _getStaffMemberByUsernameQuery.GetStaffMember(username);
            if (existingAccount is null)
            {
                throw new UserNotFoundException(username);
            }

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(existingAccount.PasswordHash, password);
            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException(username, password);
            }

            return existingAccount;
        }

        public async Task<StaffMemberRegistrationResult> Register(StaffMember newAccount, string confirmPassword)
        {
            StaffMemberRegistrationResult result = StaffMemberRegistrationResult.Success;

            if (newAccount.PasswordHash != confirmPassword)
            {
                result = StaffMemberRegistrationResult.PasswordsDoNotMatch;
            }

            StaffMember? emailAccount = await _getStaffMemberByEmailQuery.GetStaffMember(newAccount.Email);
            if (emailAccount is not null)
            {
                result = StaffMemberRegistrationResult.EmailAlreadyExists;
            }

            StaffMember? usernameAccount = await _getStaffMemberByUsernameQuery.GetStaffMember(newAccount.Username);
            if (usernameAccount is not null)
            {
                result = StaffMemberRegistrationResult.UsernameAlreadyExists;
            }

            if (result == StaffMemberRegistrationResult.Success)
            {
                newAccount.PasswordHash = _passwordHasher.HashPassword(newAccount.PasswordHash);
                await _createStaffMemberCommand.Create(newAccount);
            }

            return result;
        }
    }
}