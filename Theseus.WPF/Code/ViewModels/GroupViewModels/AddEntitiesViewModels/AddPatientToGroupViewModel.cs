using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;
using Theseus.Domain.CommandInterfaces.PatientCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.PatientQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Commands.GroupCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Groups;

namespace Theseus.WPF.Code.ViewModels
{
    public class AddPatientToGroupViewModel : ViewModelBase
    {
        private string _enteredUsername = string.Empty;
        public string EnteredUsername
        {
            get => _enteredUsername;
            set
            {
                _enteredUsername = value;
                OnPropertyChanged(nameof(EnteredUsername));
            }
        }

        private Patient? _patient = null;
        public Patient? Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }

        private bool _patientWasFound = false;
        public bool PatientWasFound
        {
            get => _patientWasFound;
            set
            {
                _patientWasFound = value;
                OnPropertyChanged(nameof(PatientWasFound));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        private bool _patientIsInGroup = false;
        public bool PatientIsInGroup
        {
            get => _patientIsInGroup;
            set
            {
                _patientIsInGroup = value;
                OnPropertyChanged(nameof(PatientIsInGroup));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        public bool CanAdd => PatientWasFound && !PatientIsInGroup;

        public Guid SelectedGroupId { get; }

        public ICommand SearchForPatient { get; }
        public ICommand AddPatientToGroup { get; }

        public AddPatientToGroupViewModel(SelectedGroupDetailsStore selectedGroupDetailsStore,
                                          IGetPatientByUsernameQuery getPatientByUsernameQuery,
                                          IGetGroupByPatientQuery getGroupByPatientQuery,
                                          IAddPatientToGroupCommand addPatientToGroupCommand,
                                          NavigationService<GroupDetailsViewModel> groupDetailsNavigationService)
        {
            PropertyChanged += OnPropertyChanged;

            SelectedGroupId = selectedGroupDetailsStore.SelectedGroup.Id;
            SearchForPatient = new SearchForPatientCommand(this, getPatientByUsernameQuery, getGroupByPatientQuery);
            AddPatientToGroup = new AddPatientToGroupCommand(this, addPatientToGroupCommand, groupDetailsNavigationService);
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Patient))
            {
                PatientWasFound = Patient is not null;
                PatientIsInGroup = Patient?.Group is not null;
            }
        }
    }
}