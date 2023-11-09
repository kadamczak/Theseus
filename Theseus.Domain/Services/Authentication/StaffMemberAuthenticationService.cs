using Microsoft.AspNet.Identity;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces;

namespace Theseus.Domain.Services.Authentication
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
            this._getStaffMemberByUsernameQuery = getStaffMemberByUsernameQuery;
            this._getStaffMemberByEmailQuery = getStaffMemberByEmailQuery;
            this._createStaffMemberCommand = createStaffMemberCommand;
            this._passwordHasher = passwordHasher;
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

        public async Task<RegistrationResult> Register(StaffMember newAccount, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if (newAccount.PasswordHash != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            StaffMember? emailAccount = await _getStaffMemberByEmailQuery.GetStaffMember(newAccount.Email);
            if (emailAccount is not null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            StaffMember? usernameAccount = await _getStaffMemberByUsernameQuery.GetStaffMember(newAccount.Username);
            if (usernameAccount is not null)
            {
                result = RegistrationResult.UsernameAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                newAccount.PasswordHash = _passwordHasher.HashPassword(newAccount.PasswordHash);
                await _createStaffMemberCommand.Create(newAccount);
            }

            return result;
        }
    }
}