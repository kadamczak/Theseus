using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.GroupRelated.Exceptions;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Xunit;

namespace Theseus.Domain.Services.Authentication.PatientAuthentication.Tests
{
    public class PatientAuthenticationServiceTests
    {
        private IGetPatientByUsernameQuery _mockGetPatientByUsernameQuery;
        private IGetGroupByNameQuery _mockGetGroupByNameQuery;
        private IGetGroupByPatientQuery _mockGetGroupByPatientQuery;
        private ICreatePatientCommand _mockCreatePatientCommand;

        private PatientAuthenticationService _patientAuthenticationService;

        public PatientAuthenticationServiceTests()
        {
            _mockGetPatientByUsernameQuery = Substitute.For<IGetPatientByUsernameQuery>();
            _mockGetGroupByNameQuery = Substitute.For<IGetGroupByNameQuery>();
            _mockGetGroupByPatientQuery = Substitute.For<IGetGroupByPatientQuery>();
            _mockCreatePatientCommand = Substitute.For<ICreatePatientCommand>();

            _patientAuthenticationService = new PatientAuthenticationService(_mockGetPatientByUsernameQuery, _mockGetGroupByNameQuery, _mockGetGroupByPatientQuery, _mockCreatePatientCommand);
        }

        //=======================================================================
        //async Task<Patient> Login(string username, string groupName)
        //=======================================================================
        [Fact()]
        public async Task Login_ShouldReturnSuccess_WhenUniqueUsernameAndGroupExistsAndDataPassesDataAnnotationChecks()
        {
            //arrange
            string username = "user_22";
            string groupName = "a-gr";

            Patient existingPatient = new Patient() { Username = username };
            _mockGetPatientByUsernameQuery.GetPatient(username).Returns(existingPatient);

            Group existingGroup = new Group() { Name = groupName };
            _mockGetGroupByPatientQuery.GetGroup(existingPatient.Id).Returns(existingGroup);

            //act
            Patient patientResult = await _patientAuthenticationService.Login(username, groupName);

            //assert
            Assert.Equal(username, patientResult.Username);
        }

        [Fact()]
        public async Task Login_ShouldThrowUserNotFoundException_WhenAccountWithUsernameDoesNotExist()
        {
            //arrange
            string username = "user222";
            string groupName = "a-gr";

            _mockGetPatientByUsernameQuery.GetPatient(username).ReturnsNull();

            //act
            //assert
            try
            {
                await _patientAuthenticationService.Login(username, groupName);
                Assert.Fail("Should throw UserNotFoundException");
            }
            catch (UserNotFoundException)
            {
                Assert.True(true);
            }
        }

        [Fact()]
        public async Task Login_ShouldThrowWrongGroupNameForPatientException_WhenPatientIsNotInAnyGroup()
        {
            //arrange
            string username = "user222";
            string groupName = "a-gr";

            Patient existingPatient = new Patient() { Username = username };
            _mockGetPatientByUsernameQuery.GetPatient(username).Returns(existingPatient);
            _mockGetGroupByPatientQuery.GetGroup(existingPatient.Id).ReturnsNull();

            //act
            //assert
            try
            {
                await _patientAuthenticationService.Login(username, groupName);
                Assert.Fail("Should throw WrongGroupNameForPatientException");
            }
            catch (WrongGroupNameForPatientException)
            {
                Assert.True(true);
            }
        }

        [Fact()]
        public async Task Login_ShouldThrowWrongGroupNameForPatientException_WhenPatientIsInAnotherGroup()
        {
            //arrange
            string username = "user222";
            string groupName = "a-gr";

            Patient existingPatient = new Patient() { Username = username };
            _mockGetPatientByUsernameQuery.GetPatient(username).Returns(existingPatient);
            Group existingGroup = new Group() { Name = "bbb-gr" };
            _mockGetGroupByPatientQuery.GetGroup(existingPatient.Id).Returns(existingGroup);

            //act
            //assert
            try
            {
                await _patientAuthenticationService.Login(username, groupName);
                Assert.Fail("Should throw WrongGroupNameForPatientException");
            }
            catch (WrongGroupNameForPatientException)
            {
                Assert.True(true);
            }
        }
        //=======================================================================
        //async Task<PatientRegistrationResult> Register(Patient newAccount, string groupName)
        //=======================================================================

        [Fact()]
        public async Task Register_ShouldReturnSuccess_WhenUniqueUsernameAndGroupExistsAndDataPassesDataAnnotationChecks()
        {
            //arrange
            Patient newAccount = new Patient()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                DateCreated = DateTime.Now
            };
            string groupName = "a-gr";

            Group existingGroup = new Group() { Name = groupName };

            _mockGetPatientByUsernameQuery.GetPatient(newAccount.Username).ReturnsNull();
            _mockGetGroupByNameQuery.GetGroup(groupName).Returns(existingGroup);

            //act
            PatientRegistrationResult result = await _patientAuthenticationService.Register(newAccount, groupName);

            //assert
            Assert.Equal(PatientRegistrationResult.Success, result);
        }

        public static IEnumerable<object[]> PatientInvalid_Data => new List<object[]>
            {
                new object[] { new Patient() {
                    Id = Guid.NewGuid(),
                    Username = "user222   b",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new Patient() {
                    Id = Guid.NewGuid(),
                    Username = "user222!",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new Patient() {
                    Id = Guid.NewGuid(),
                    Username = "",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new Patient() {
                    Id = Guid.NewGuid(),
                    Username = "user222wwwwwwwwwwwwwwwwwwwwww",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new Patient() {
                    Id = Guid.NewGuid(),
                    Username = "   u",
                    DateCreated = DateTime.Now
                    }
                },
                new object[] { new Patient() {
                    Id = Guid.NewGuid(),
                    Username = ".",
                    DateCreated = DateTime.Now
                    }
                },
            };

        [Theory]
        [MemberData(nameof(PatientInvalid_Data))]
        public async Task Register_ShouldReturnPatientDataNotValid_WhenInvalidData(Patient newAccount)
        {
            //arrange
            string groupName = "a-gr";

            //act
            PatientRegistrationResult result = await _patientAuthenticationService.Register(newAccount, groupName);

            //assert
            Assert.Equal(PatientRegistrationResult.PatientDataNotValid, result);
        }

        [Fact()]
        public async Task Register_ShouldReturnUsernameAlreadyExists_WhenPatientWithThisUsernameExists()
        {
            //arrange
            Patient newAccount = new Patient()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                DateCreated = DateTime.Now
            };
            string groupName = "a-gr";

            Group existingGroup = new Group() { Name = groupName };
            Patient existingAccount = new Patient() { Username = newAccount.Username };

            _mockGetPatientByUsernameQuery.GetPatient(newAccount.Username).Returns(existingAccount);
            _mockGetGroupByNameQuery.GetGroup(groupName).Returns(existingGroup);

            //act
            PatientRegistrationResult result = await _patientAuthenticationService.Register(newAccount, groupName);

            //assert
            Assert.Equal(PatientRegistrationResult.UsernameAlreadyExists, result);
        }

        [Fact()]
        public async Task Register_ShouldReturnGroupDoesNotExist_WhenGroupWithNameDoesNotAlreadyExist()
        {
            //arrange
            Patient newAccount = new Patient()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                DateCreated = DateTime.Now
            };
            string groupName = "a-gr";

            _mockGetPatientByUsernameQuery.GetPatient(newAccount.Username).ReturnsNull();
            _mockGetGroupByNameQuery.GetGroup(groupName).ReturnsNull();

            //act
            PatientRegistrationResult result = await _patientAuthenticationService.Register(newAccount, groupName);

            //assert
            Assert.Equal(PatientRegistrationResult.GroupDoesNotExist, result);
        }

        [Fact()]
        public async Task Register_ShouldReturnConnectionFailed_WhenExceptionOccurs()
        {
            //arrange
            Patient newAccount = new Patient()
            {
                Id = Guid.NewGuid(),
                Username = "user222",
                DateCreated = DateTime.Now
            };
            string groupName = "a-gr";

            _mockGetPatientByUsernameQuery.GetPatient(newAccount.Username).ReturnsNull();
            _mockGetGroupByNameQuery.GetGroup(groupName).Throws(new Exception("Message"));

            //act
            PatientRegistrationResult result = await _patientAuthenticationService.Register(newAccount, groupName);

            //assert
            Assert.Equal(PatientRegistrationResult.ConnectionFailed, result);
        }
    }
}