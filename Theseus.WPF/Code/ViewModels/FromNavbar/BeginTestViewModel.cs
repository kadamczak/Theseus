using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Navigation;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Exams;

namespace Theseus.WPF.Code.ViewModels
{
    public class BeginTestViewModel : ViewModelBase
    {
        public ObservableCollection<ExamSet> AvailableExamSets { get; set; } = new ObservableCollection<ExamSet>();

        private ExamSet _selectedExamSet;
        public ExamSet SelectedExamSet
        {
            get => _selectedExamSet;
            set
            {
                _selectedExamSet = value;
                OnPropertyChanged(nameof(SelectedExamSet));
            }
        }

        public ICommand BeginExam { get; }

        public BeginTestViewModel(CurrentExamStore currentExamStore,
                                  ICurrentPatientStore currentPatientStore,
                                  IGetGroupByPatientQuery getGroupQuery,
                                  IGetExamSetsOfGroupQuery getExamSetsQuery,
                                  IGetOrderedMazesWithSolutionOfExamSetQuery getMazesOfExamSetQuery,
                                  NavigationService<ExamPageViewModel> examPageNavigationService)
        {
            if (!currentPatientStore.IsPatientLoggedIn)
                return;

            var examSets = GetAvailableExamSets(currentPatientStore, getGroupQuery, getExamSetsQuery);
            AvailableExamSets = new ObservableCollection<ExamSet>(examSets);
            SelectedExamSet = AvailableExamSets.FirstOrDefault();

            BeginExam = new BeginExamCommand(this, currentExamStore, getMazesOfExamSetQuery, examPageNavigationService);
        }

        private IEnumerable<ExamSet> GetAvailableExamSets(ICurrentPatientStore currentPatientStore,
                                                          IGetGroupByPatientQuery getGroupQuery,
                                                          IGetExamSetsOfGroupQuery getExamSetsQuery)
        {
            Group group = getGroupQuery.GetGroup(currentPatientStore.Patient.Id);
            return getExamSetsQuery.GetExamSets(group!.Id);
        }
    }
}