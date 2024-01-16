using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Theseus.Domain.Services.Authentication.PatientAuthentication;

namespace Theseus.Domain.Services.Authentication.StaffMemberAuthentication
{
    /// <summary>
    /// The <c>StaffMemberAuthenticationService</c> class implements <c>StaffMember</c> login and registration.
    /// </summary>
    public class StaffMemberAuthenticationService : IStaffMemberAuthenticationService
    {
        private readonly IGetStaffMemberByUsernameQuery _getStaffMemberByUsernameQuery;
        private readonly IGetStaffMemberByEmailQuery _getStaffMemberByEmailQuery;
        private readonly ICreateStaffMemberCommand _createStaffMemberCommand;
        private readonly ICreateGroupCommand _createGroupCommand;
        private readonly IPasswordHasher _passwordHasher;

        public StaffMemberAuthenticationService(IGetStaffMemberByUsernameQuery getStaffMemberByUsernameQuery,
                                     IGetStaffMemberByEmailQuery getStaffMemberByEmailQuery,
                                     ICreateStaffMemberCommand createStaffMemberCommand,
                                     ICreateGroupCommand createGroupCommand,
                                     IPasswordHasher passwordHasher)
        {
            _getStaffMemberByUsernameQuery = getStaffMemberByUsernameQuery;
            _getStaffMemberByEmailQuery = getStaffMemberByEmailQuery;
            _createStaffMemberCommand = createStaffMemberCommand;
            _createGroupCommand = createGroupCommand;
            _passwordHasher = passwordHasher;
        }

        public async Task<StaffMember> Login(string username, string password)
        {
            StaffMember? existingAccount = await _getStaffMemberByUsernameQuery.GetStaffMember(username) ?? throw new UserNotFoundException(username);

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
                return result;
            }

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(newAccount, null, null);
            if (!Validator.TryValidateObject(newAccount, validationContext, validationResults, true))
            {
                return StaffMemberRegistrationResult.StaffMemberDataNotValid;
            }

            try
            {
                StaffMember? emailAccount = await _getStaffMemberByEmailQuery.GetStaffMember(newAccount.Email);
                if (emailAccount is not null)
                {
                    result = StaffMemberRegistrationResult.EmailAlreadyExists;
                    return result;
                }

                StaffMember? usernameAccount = await _getStaffMemberByUsernameQuery.GetStaffMember(newAccount.Username);
                if (usernameAccount is not null)
                {
                    result = StaffMemberRegistrationResult.UsernameAlreadyExists;
                    return result;
                }

                if (result == StaffMemberRegistrationResult.Success)
                {
                    newAccount.PasswordHash = _passwordHasher.HashPassword(newAccount.PasswordHash);
                    await _createStaffMemberCommand.Create(newAccount);

                    Group defaultGroup = CreateDefaultGroupForStaffMember(newAccount);
                    defaultGroup.Owner = newAccount;
                    defaultGroup.StaffMembers.Add(newAccount);
                    await _createGroupCommand.CreateGroup(defaultGroup);
                }
            }
            catch (Exception)
            {
                return StaffMemberRegistrationResult.ConnectionFailed;
            }

            return result;
        }

        private Group CreateDefaultGroupForStaffMember(StaffMember staffMember)
        {
            return new Group()
            {
                Id = Guid.NewGuid(),
                Name = staffMember.Username + "-gr",
                Owner = staffMember
            };
        }
    }
}