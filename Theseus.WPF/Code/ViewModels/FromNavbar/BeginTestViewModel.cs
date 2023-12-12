using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.QueryInterfaces.ExamSetQueryInterfaces;
using Theseus.Domain.QueryInterfaces.GroupQueryInterfaces;
using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamCommands;
using Theseus.WPF.Code.Extensions;
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

        private bool _isPatientLoggedIn = false;
        public bool IsPatientLoggedIn
        {
            get => _isPatientLoggedIn;
            set
            {
                _isPatientLoggedIn = value;
                OnPropertyChanged(nameof(IsPatientLoggedIn));
            }
        }

        private string _examSetsOmittedDueToScreenSizeText = string.Empty;
        public string ExamSetsOmittedDueToScreenSizeText
        {
            get => _examSetsOmittedDueToScreenSizeText;
            set
            {
                _examSetsOmittedDueToScreenSizeText = value;
                OnPropertyChanged(nameof(ExamSetsOmittedDueToScreenSizeText));
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
            IsPatientLoggedIn = currentPatientStore.IsPatientLoggedIn;
            BeginExam = new BeginExamCommand(this, currentExamStore, currentPatientStore, getMazesOfExamSetQuery, examPageNavigationService);

            if (!currentPatientStore.IsPatientLoggedIn)
                return;

            Group? patientGroup = getGroupQuery.GetGroup(currentPatientStore.Patient.Id);
            if (patientGroup is null)
                return;

            var examSets = GetAvailableFittedExamSets(currentPatientStore, patientGroup!, getExamSetsQuery, getMazesOfExamSetQuery);
            AvailableExamSets = new ObservableCollection<ExamSet>(examSets);
            SelectedExamSet = AvailableExamSets.FirstOrDefault();
        }

        private List<ExamSet> GetAvailableFittedExamSets(ICurrentPatientStore currentPatientStore,
                                                         Group group,
                                                         IGetExamSetsOfGroupQuery getExamSetsQuery,
                                                         IGetOrderedMazesWithSolutionOfExamSetQuery getMazesOfExamSetQuery)
        {
            var allAvailableExamSets = getExamSetsQuery.GetExamSets(group!.Id);
            var displayableExamSets = GetExamSetsCompatibleWithScreenSize(allAvailableExamSets, getMazesOfExamSetQuery);

            int amountOfOmittedExamSets = allAvailableExamSets.Count() - displayableExamSets.Count();
            ExamSetsOmittedDueToScreenSizeText = $"{"AmountOfSetsOmitted".Resource()}{amountOfOmittedExamSets}.\n{"YouCanChangeTheMinimalCellSize".Resource()}";

            return displayableExamSets;
        }

        private List<ExamSet> GetExamSetsCompatibleWithScreenSize(IEnumerable<ExamSet> examSets, IGetOrderedMazesWithSolutionOfExamSetQuery getMazesOfExamSetQuery)
        {
            List<ExamSet> displayableExamSets = new List<ExamSet>();
            float minCellSize = Properties.Settings.Default.MinimalCellSize;

            int maxAllowedColumnAmount = (int)((System.Windows.SystemParameters.PrimaryScreenWidth - 340) / (minCellSize + 2));
            int maxAllowedRowAmount = (int)(System.Windows.SystemParameters.PrimaryScreenHeight / (minCellSize + 2));

            foreach (var examSet in examSets)
            {
                var mazes = getMazesOfExamSetQuery.GetMazesWithSolution(examSet.Id);

                int highestColumnAmount = mazes.OrderByDescending(m => m.Grid.ColumnAmount).FirstOrDefault().Grid.ColumnAmount;
                int highestRowAmount = mazes.OrderByDescending(m => m.Grid.RowAmount).FirstOrDefault().Grid.RowAmount;

                if (highestColumnAmount <= maxAllowedColumnAmount && highestRowAmount <= maxAllowedRowAmount)
                {
                    displayableExamSets.Add(examSet);
                }
            }

            return displayableExamSets;
        }
    }
}