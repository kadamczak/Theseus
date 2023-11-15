using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Enums;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings;
using Theseus.WPF.Code.ViewModels.Bindings.AccountBindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientDetailsLoggedInViewModel : ViewModelBase
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

        private string _age = string.Empty;
        private SexViewModel _selectedSex;
        private ProfessionTypeViewModel _selectedProfessionType;
        private EducationLevelViewModel _selectedEducationLevel;

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

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

        public Patient CurrentPatient { get; }
        
        public ICommand Save { get; }
        public ICommand Logout { get; }

        public PatientDetailsLoggedInViewModel(IPatientAuthenticator authenticator,
                                               IUpdatePatientCommand updatePatientCommand,
                                               NavigationService<PatientLoginRegisterViewModel> patientLoginRegisterNavigationService)
        {
            if (!authenticator.IsLoggedInAsPatient)
                return;

            CurrentPatient = authenticator.CurrentPatient!;
            LoadCurrentPatientInfoToViewModel();

            Save = new SavePatientInfoCommand(this, updatePatientCommand);
            Logout = new LogoutPatientCommand(authenticator, patientLoginRegisterNavigationService);
        }

        private void LoadCurrentPatientInfoToViewModel()
        {
            this._username = CurrentPatient.Username;
            this._age = Convert.ToString(CurrentPatient.Age);
            this._selectedSex = Sexes.Where(a => a.Value == CurrentPatient.Sex).First();
            this._selectedEducationLevel = EducationLevels.Where(a => a.Value == CurrentPatient.EducationLevel).First();
            this._selectedProfessionType = ProfessionTypes.Where(a => a.Value == CurrentPatient.ProfessionType).First();
        }

        public void UpdateCurrentPatientInfoFromViewModel()
        {
            CurrentPatient.Age = StringToNullableInt(Age);
            CurrentPatient.Sex = SelectedSex.Value;
            CurrentPatient.EducationLevel = SelectedEducationLevel.Value;
            CurrentPatient.ProfessionType = SelectedProfessionType.Value;
        }

        public bool CheckIfPatientCanSaveChanges()
        {
            if (!IsAgeValid())
                return false;

            var currentPatientInfo = (CurrentPatient.Age, CurrentPatient.Sex, CurrentPatient.EducationLevel, CurrentPatient.ProfessionType);
            var infoFromViewModel = (StringToNullableInt(Age), SelectedSex.Value, SelectedEducationLevel.Value, SelectedProfessionType.Value);

            return currentPatientInfo != infoFromViewModel;
        }

        public bool IsAgeValid()
        {
            if (int.TryParse(Age, out int ageValue))
            {
                if (!AgeHasValidValue(ageValue))
                    return false;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Age))
                    return false;
            }

            return true;
        }

        private bool AgeHasValidValue(int age) => age > 0 && age <= 125;

        private int? StringToNullableInt(string str) => (string.IsNullOrWhiteSpace(str)) ? null : int.Parse(str);
    }
}