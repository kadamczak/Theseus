using System.Collections.ObjectModel;
using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Enums;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels.AccountViewModels.Interfaces;
using Theseus.WPF.Code.ViewModels.Bindings;
using Theseus.WPF.Code.ViewModels.Bindings.AccountBindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientDetailsLoggedInViewModel : AccountDetailsViewModel
    {
        private string _username = string.Empty;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _age = string.Empty;

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public ObservableCollection<SexViewModel> Sexes { get; } = new ObservableCollection<SexViewModel> {
                                new SexViewModel("Do Not Disclose", Sex.Undisclosed),
                                new SexViewModel("Male", Sex.Male),
                                new SexViewModel("Female", Sex.Female),
                                };

        public ObservableCollection<ProfessionTypeViewModel> ProfessionTypes { get; } = new ObservableCollection<ProfessionTypeViewModel> {
                                new ProfessionTypeViewModel("Do Not Disclose", ProfessionType.Undisclosed),
                                new ProfessionTypeViewModel("None", ProfessionType.None),
                                new ProfessionTypeViewModel("Physical", ProfessionType.Physical),
                                new ProfessionTypeViewModel("Mental", ProfessionType.Mental)
                                };

        public ObservableCollection<EducationLevelViewModel> EducationLevels { get; } = new ObservableCollection<EducationLevelViewModel> {
                                new EducationLevelViewModel("Do Not Disclose", EducationLevel.Undisclosed),
                                new EducationLevelViewModel("Primary", EducationLevel.Primary),
                                new EducationLevelViewModel("Lower Secondary", EducationLevel.LowerSecondary),
                                new EducationLevelViewModel("Secondary", EducationLevel.Secondary),
                                new EducationLevelViewModel("Vocational", EducationLevel.Vocational),
                                new EducationLevelViewModel("Higher", EducationLevel.Higher)
                                };

        private SexViewModel _selectedSex;
        private ProfessionTypeViewModel _selectedProfessionType;
        private EducationLevelViewModel _selectedEducationLevel;

        public SexViewModel SelectedSex
        {
            get => _selectedSex;
            set
            {
                _selectedSex = value;
                OnPropertyChanged(nameof(SelectedSex));
            }
        }

        public ProfessionTypeViewModel SelectedProfessionType
        {
            get => _selectedProfessionType;
            set
            {
                _selectedProfessionType = value;
                OnPropertyChanged(nameof(SelectedProfessionType));
            }
        }

        public EducationLevelViewModel SelectedEducationLevel
        {
            get => _selectedEducationLevel;
            set
            {
                _selectedEducationLevel = value;
                OnPropertyChanged(nameof(SelectedEducationLevel));
            }
        }

        public ICommand Logout { get; }

        public PatientDetailsLoggedInViewModel(IPatientAuthenticator authenticator,
                                               NavigationService<PatientLoginRegisterViewModel> patientLoginRegisterNavigationService)
        {
            if (authenticator.IsLoggedInAsPatient)
                LoadPatientInfo(authenticator.CurrentPatient!);

            Logout = new LogoutPatientCommand(authenticator, patientLoginRegisterNavigationService);
        }

        private void LoadPatientInfo(Patient patient)
        {
            this.Username = patient.Username;
            this.Age = (patient.Age is null) ? string.Empty : patient.Age.ToString();

        }
    }
}