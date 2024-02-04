using Microsoft.AspNet.Identity;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.CommandInterfaces.StaffMemberCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.StaffMemberQueryInterfaces;
using Xunit;

namespace Theseus.Domain.Services.Authentication.StaffMemberAuthentication.Tests
{
    public class StaffMemberAuthenticationServiceTests
    {
        private IPasswordHasher _mockPasswordHasher;
        private IGetStaffMemberByUsernameQuery _mockGetStaffMemberByUsernameQuery;
        private IGetStaffMemberByEmailQuery _mockGetStaffMemberByEmailQuery;
        private ICreateStaffMemberCommand _mockCreateStaffMemberCommand;
        private ICreateGroupCommand _mockCreateGroupCommand;

        private StaffMemberAuthenticationService _staffMemberAuthenticationService;

        public StaffMemberAuthenticationServiceTests()
        {
            _mockPasswordHasher = Substitute.For<IPasswordHasher>();
            _mockGetStaffMemberByUsernameQuery = Substitute.For<IGetStaffMemberByUsernameQuery>();
            _mockGetStaffMemberByEmailQuery = Substitute.For<IGetStaffMemberByEmailQuery>();
            _mockCreateStaffMemberCommand = Substitute.For<ICreateStaffMemberCommand>();
            _mockCreateGroupCommand = Substitute.For<ICreateGroupCommand>();

            _staffMemberAuthenticationService = new StaffMemberAuthenticationService(_mockGetStaffMemberByUsernameQuery,
                                                                                     _mockGetStaffMemberByEmailQuery,
                                                                                     _mockCreateStaffMemberCommand,
                                                                                     _mockCreateGroupCommand,
                                                                                     _mockPasswordHasher); 
        }

        //=======================================================================
        //async Task<StaffMember> Login(string username, string password)
        //=======================================================================
        [Fact()]
        public async Task Login_ShouldReturnSuccess_WhenAccountWithUsernameExistsAndPasswordMatches()
        {
            //arrange
            string username = "user222";
            string password = "9999999bB";

            StaffMember existingStaffMember = new StaffMember() { Username = username };
            _mockGetStaffMemberByUsernameQuery.GetStaffMember(username).Returns(existingStaffMember);
            _mockPasswordHasher.VerifyHashedPassword(Arg.Any<string>(), password).Returns(PasswordVerificationResult.Success);

            //act
            StaffMember staffMemberResult = await _staffMemberAuthenticationService.Login(username, password);

            //assert
            Assert.Equal(username, staffMemberResult.Username);
        }

        [Fact()]
        public async Task Login_ShouldThrowUserNotFoundException_WhenAccountWithUsernameDoesNotExist()
        {
            //arrange
            string username = "user222";
            string password = "9999999bB";

            _mockGetStaffMemberByUsernameQuery.GetStaffMember(username).ReturnsNull();

            //act
            //assert
            try
            {
                await _staffMemberAuthenticationService.Login(username, password);
                Assert.Fail("Should throw UserNotFoundException");
            }
            catch (UserNotFoundException)
            {
                Assert.True(true);
            }
        }

        [Fact()]
        public async Task Login_ShouldThrowInvalidPasswordException_WhenAccountWithUsernameExistsButPasswordDoesNotMatch()
        {
            //arrange
            string username = "user222";
            string password = "9999999bB";

            StaffMember existingStaffMember = new StaffMember() { Username = username };
            _mockGetStaffMemberByUsernameQuery.GetStaffMember(username).Returns(existingStaffMember);
            _mockPasswordHasher.VerifyHashedPassword(Arg.Any<string>(), password).Returns(PasswordVerificationResult.Failed);

            //act
            //assert
            try
            {
                await _staffMemberAuthenticationService.Login(username, password);
                Assert.Fail("Should throw InvalidPasswordException");
            }
            catch(InvalidPasswordException)
            {
                Assert.True(true);
            }
        }

        //=======================================================================
        //async Task<StaffMemberRegistrationResult> Register(StaffMember newAccount, string confirmPassword)
        //=======================================================================

        [Fact()]
        public async Task Register_ShouldReturnSuccess_WhenUniqueUsernameAndEmailAndPasswordsMatchAndDataPassesDataAnnotationChecks()
        {
            //arrange
            StaffMember newAccount = new StaffMember()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                PasswordHash = "9999999bB",
                Name = "Abc",
                Surname = "Xyz",
                Email = "aaa@gmail.com",
                DateCreated = DateTime.Now
            };
            string confirmPassword = "9999999bB";

            _mockGetStaffMemberByEmailQuery.GetStaffMember(newAccount.Email).ReturnsNull();
            _mockGetStaffMemberByUsernameQuery.GetStaffMember(newAccount.Username).ReturnsNull();

            //act
            StaffMemberRegistrationResult result = await _staffMemberAuthenticationService.Register(newAccount, confirmPassword);

            //assert
            Assert.Equal(StaffMemberRegistrationResult.Success, result);
        }

        public static IEnumerable<object[]> StaffMemberInvalid_Data => new List<object[]>
            {
                new object[] { new StaffMember() {
                    Id = Guid.NewGuid(),
                    Username = "user222   b",
                    PasswordHash = "9999999bB",
                    Name = "Abc",
                    Surname = "Xyz",
                    Email = "aaa@gmail.com",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new StaffMember() {
                    Id = Guid.NewGuid(),
                    Username = "user!",
                    PasswordHash = "9999999bB",
                    Name = "Abc",
                    Surname = "Xyz",
                    Email = "aaa@gmail.com",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new StaffMember() {
                    Id = Guid.NewGuid(),
                    Username = "user222",
                    PasswordHash = "9999999bB",
                    Name = "",
                    Surname = "Xyz",
                    Email = "aaa@gmail.com",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new StaffMember() {
                    Id = Guid.NewGuid(),
                    Username = "user222wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww",
                    PasswordHash = "9999999bB",
                    Name = "Abc",
                    Surname = "Xyz",
                    Email = "aaa@gmail.com",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new StaffMember() {
                    Id = Guid.NewGuid(),
                    Username = "user222",
                    PasswordHash = "9999999bB",
                    Name = "Abc",
                    Surname = "Xyz",
                    Email = "aaaaaaa",
                    DateCreated = DateTime.Now
                    }
                },
            };

        [Theory]
        [MemberData(nameof(StaffMemberInvalid_Data))]
        public async Task Register_ShouldReturnStaffMemberNotValid_WhenDataDoesNotPassDataAnnotationChecks(StaffMember newAccount)
        {
            //arrange
            string confirmPassword = "9999999bB";

            //act
            StaffMemberRegistrationResult result = await _staffMemberAuthenticationService.Register(newAccount, confirmPassword);

            //assert
            Assert.Equal(StaffMemberRegistrationResult.StaffMemberDataNotValid, result);
        }

        [Fact()]
        public async Task Register_ShouldReturnPasswordsDoNotMatch_WhenPasswordAndConfirmPasswordDoNotMatch()
        {
            //arrange
            StaffMember newAccount = new StaffMember()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                PasswordHash = "9999999bB",
                Name = "Abc",
                Surname = "Xyz",
                Email = "aaa@gmail.com",
                DateCreated = DateTime.Now
            };
            string confirmPassword = "99911111111BBb";

            _mockGetStaffMemberByEmailQuery.GetStaffMember(newAccount.Email).ReturnsNull();
            _mockGetStaffMemberByUsernameQuery.GetStaffMember(newAccount.Username).ReturnsNull();

            //act
            StaffMemberRegistrationResult result = await _staffMemberAuthenticationService.Register(newAccount, confirmPassword);

            //assert
            Assert.Equal(StaffMemberRegistrationResult.PasswordsDoNotMatch, result);
        }

        [Fact()]
        public async Task Register_ShouldReturnEmailAlreadyExists_WhenStaffMemberWithThisEmailExists()
        {
            //arrange
            StaffMember newAccount = new StaffMember()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                PasswordHash = "9999999bB",
                Name = "Abc",
                Surname = "Xyz",
                Email = "aaa@gmail.com",
                DateCreated = DateTime.Now
            };
            string confirmPassword = "9999999bB";

            _mockGetStaffMemberByEmailQuery.GetStaffMember(newAccount.Email).Returns(new StaffMember() { Email = newAccount.Email });
            _mockGetStaffMemberByUsernameQuery.GetStaffMember(newAccount.Username).ReturnsNull();

            //act
            StaffMemberRegistrationResult result = await _staffMemberAuthenticationService.Register(newAccount, confirmPassword);

            //assert
            Assert.Equal(StaffMemberRegistrationResult.EmailAlreadyExists, result);
        }

        [Fact()]
        public async Task Register_ShouldReturnUsernameAlreadyExists_WhenStaffMemberWithThisUsernameExists()
        {
            //arrange
            StaffMember newAccount = new StaffMember()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                PasswordHash = "9999999bB",
                Name = "Abc",
                Surname = "Xyz",
                Email = "aaa@gmail.com",
                DateCreated = DateTime.Now
            };
            string confirmPassword = "9999999bB";

            _mockGetStaffMemberByEmailQuery.GetStaffMember(newAccount.Email).ReturnsNull();
            _mockGetStaffMemberByUsernameQuery.GetStaffMember(newAccount.Username).Returns(new StaffMember { Username = newAccount.Username });

            //act
            StaffMemberRegistrationResult result = await _staffMemberAuthenticationService.Register(newAccount, confirmPassword);

            //assert
            Assert.Equal(StaffMemberRegistrationResult.UsernameAlreadyExists, result);
        }

        [Fact()]
        public async Task Register_ShouldReturnConnectionFailed_WhenExceptionOccurs()
        {
            //arrange
            StaffMember newAccount = new StaffMember()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                PasswordHash = "9999999bB",
                Name = "Abc",
                Surname = "Xyz",
                Email = "aaa@gmail.com",
                DateCreated = DateTime.Now
            };
            string confirmPassword = "9999999bB";

            _mockGetStaffMemberByEmailQuery.GetStaffMember(newAccount.Email).ReturnsNull();
            _mockGetStaffMemberByUsernameQuery.GetStaffMember(newAccount.Username).Throws(new Exception("Message"));

            //act
            StaffMemberRegistrationResult result = await _staffMemberAuthenticationService.Register(newAccount, confirmPassword);

            //assert
            Assert.Equal(StaffMemberRegistrationResult.ConnectionFailed, result);
        }
    }
}