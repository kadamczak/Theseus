using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Enums;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings;
using Theseus.WPF.Code.ViewModels.Bindings.AccountBindings;

namespace Theseus.WPF.Code.ViewModels
{
    /// <summary>
    /// The <c>PatientDetailsLoggedInViewModel</c> class contains bindings for Patient Details Logged In View.
    /// </summary>
    public class PatientDetailsLoggedInViewModel : ErrorCheckingViewModel
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
                                new SexViewModel("DoNotDisclose".Resource(), Sex.Undisclosed),
                                new SexViewModel("Male".Resource(), Sex.Male),
                                new SexViewModel("Female".Resource(), Sex.Female),
                                };

        public ObservableCollection<ProfessionTypeViewModel> ProfessionTypes { get; } = new ObservableCollection<ProfessionTypeViewModel> {
                                new ProfessionTypeViewModel("DoNotDisclose".Resource(), ProfessionType.Undisclosed),
                                new ProfessionTypeViewModel("None".Resource(), ProfessionType.None),
                                new ProfessionTypeViewModel("Physical".Resource(), ProfessionType.Physical),
                                new ProfessionTypeViewModel("Mental".Resource(), ProfessionType.Mental)
                                };

        public ObservableCollection<EducationLevelViewModel> EducationLevels { get; } = new ObservableCollection<EducationLevelViewModel> {
                                new EducationLevelViewModel("DoNotDisclose".Resource(), EducationLevel.Undisclosed),
                                new EducationLevelViewModel("Primary".Resource(), EducationLevel.Primary),
                                new EducationLevelViewModel("LowerSecondary".Resource(), EducationLevel.LowerSecondary),
                                new EducationLevelViewModel("Secondary".Resource(), EducationLevel.Secondary),
                                new EducationLevelViewModel("Vocational".Resource(), EducationLevel.Vocational),
                                new EducationLevelViewModel("Higher".Resource(), EducationLevel.Higher)
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

                ClearErrors(nameof(Age));

                if (int.TryParse(Age, out int ageValue))
                {
                    if (!AgeHasValidValue(ageValue))
                        AddError(nameof(Age), "AgeOutsideOfRange".Resource());
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(Age))
                        AddError(nameof(Age), "AgeContainsInvalidCharacters".Resource());
                }

                OnPropertyChanged(nameof(CanUpdatePatient));
            }
        }

        public SexViewModel SelectedSex
        {
            get => _selectedSex;
            set
            {
                _selectedSex = value;
                OnPropertyChanged(nameof(SelectedSex));
                OnPropertyChanged(nameof(CanUpdatePatient));
            }
        }

        public ProfessionTypeViewModel SelectedProfessionType
        {
            get => _selectedProfessionType;
            set
            {
                _selectedProfessionType = value;
                OnPropertyChanged(nameof(SelectedProfessionType));
                OnPropertyChanged(nameof(CanUpdatePatient));
            }
        }

        public EducationLevelViewModel SelectedEducationLevel
        {
            get => _selectedEducationLevel;
            set
            {
                _selectedEducationLevel = value;
                OnPropertyChanged(nameof(SelectedEducationLevel));
                OnPropertyChanged(nameof(CanUpdatePatient));
            }
        }

        public Patient CurrentPatient { get; }
        
        public ICommand Save { get; }
        public ICommand Logout { get; }

        public bool CanUpdatePatient => !HasErrors && FormHasChanges();

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
            this._username = CurrentPatient.Username.Trim();
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

        public bool FormHasChanges()
        {
            var currentPatientInfo = (CurrentPatient.Age, CurrentPatient.Sex, CurrentPatient.EducationLevel, CurrentPatient.ProfessionType);
            var infoFromViewModel = (StringToNullableInt(Age), SelectedSex.Value, SelectedEducationLevel.Value, SelectedProfessionType.Value);
            return currentPatientInfo != infoFromViewModel;
        }

        private bool AgeHasValidValue(int age) => age >= 0 && age <= 125;
        private int? StringToNullableInt(string str) => (string.IsNullOrWhiteSpace(str)) ? null : int.Parse(str);
    }
}