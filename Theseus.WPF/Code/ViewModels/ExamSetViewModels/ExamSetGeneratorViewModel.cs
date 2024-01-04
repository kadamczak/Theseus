using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Domain.Models.ExamSetRelated.Enums;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.ExamSetCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels.Bindings.ExamSetBindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ExamSetGeneratorViewModel : ErrorCheckingViewModel
    {
        public ObservableCollection<ExamSetDifficultyViewModel> ExamSetDifficulties { get; } = new ObservableCollection<ExamSetDifficultyViewModel>(){
            new ExamSetDifficultyViewModel("Easier".Resource(), ExamSetDifficulty.Easy),
            new ExamSetDifficultyViewModel("Standard".Resource(), ExamSetDifficulty.Standard),
            new ExamSetDifficultyViewModel("Difficult".Resource(), ExamSetDifficulty.Difficult),
            new ExamSetDifficultyViewModel("Custom".Resource(), ExamSetDifficulty.Custom)
        };

        private ExamSetDifficultyViewModel _selectedDifficulty;

        private string _mazeAmount = "10";
        public string MazeAmount
        {
            get => _mazeAmount;
            set
            {
                _mazeAmount = value;
                OnPropertyChanged(nameof(MazeAmount));
                ClearErrors(nameof(MazeAmount));

                if (int.TryParse(MazeAmount, out int mazeAmountValue))
                {
                    if (!HasValueInRange(mazeAmountValue, MinMazeAmount, MaxMazeAmount))
                        AddError(nameof(MazeAmount), "ValueOutsideOfRange".Resource());
                }
                else
                {
                    AddError(nameof(MazeAmount), "InvalidValue".Resource());
                }

                OnPropertyChanged(nameof(CanGenerateSet));
            }
        }
        public ExamSetDifficultyViewModel SelectedDifficulty
        {
            get => _selectedDifficulty;
            set
            {
                _selectedDifficulty = value;
                OnPropertyChanged(nameof(SelectedDifficulty));
            }
        }

        private string _beginningMaxMazeDimension = "8";
        public string BeginningMaxMazeDimension
        {
            get => _beginningMaxMazeDimension;
            set
            {
                _beginningMaxMazeDimension = value;
                OnPropertyChanged(nameof(BeginningMaxMazeDimension));
                ClearErrors(nameof(BeginningMaxMazeDimension));
                ClearError(nameof(EndingMaxMazeDimension), EndingValueTooSmallMessage);

                if (int.TryParse(BeginningMaxMazeDimension, out int beginningDimensionValue))
                {
                    if (!HasValueInRange(beginningDimensionValue, MinMazeDimension, MaxMazeDimension))
                        AddError(nameof(BeginningMaxMazeDimension), "ValueOutsideOfRange".Resource());

                    if (int.TryParse(EndingMaxMazeDimension, out int endingDimensionValue))
                    {
                        if (endingDimensionValue < beginningDimensionValue)
                            AddError(nameof(EndingMaxMazeDimension), EndingValueTooSmallMessage);
                    }
                }
                else
                {
                    AddError(nameof(BeginningMaxMazeDimension), "InvalidValue".Resource());
                }

                OnPropertyChanged(nameof(CanGenerateSet));
            }
        }

        private string _endingMaxMazeDimension = "23";

        public string EndingMaxMazeDimension
        {
            get => _endingMaxMazeDimension;
            set
            {
                _endingMaxMazeDimension = value;
                OnPropertyChanged(nameof(EndingMaxMazeDimension));
                ClearErrors(nameof(EndingMaxMazeDimension));

                if (int.TryParse(EndingMaxMazeDimension, out int endingDimensionValue))
                {
                    if (!HasValueInRange(endingDimensionValue, MinMazeDimension, MaxMazeDimension))
                        AddError(nameof(EndingMaxMazeDimension), "ValueOutsideOfRange".Resource());

                    if (int.TryParse(BeginningMaxMazeDimension, out int beginningDimensionValue))
                    {
                        if (endingDimensionValue < beginningDimensionValue)
                            AddError(nameof(EndingMaxMazeDimension), EndingValueTooSmallMessage);
                    }
                }
                else
                {
                    AddError(nameof(EndingMaxMazeDimension), "InvalidValue".Resource());
                }

                OnPropertyChanged(nameof(CanGenerateSet));
            }
        }

        private readonly int MinMazeAmount = 3;
        private readonly int MaxMazeAmount = 30;

        private readonly int MinMazeDimension = 5;
        private readonly int MaxMazeDimension = 50;

        private readonly string EndingValueTooSmallMessage = "Ending value cannot be smaller than beginning value.";

        private bool HasValueInRange(int value, int min, int max) => value >= min && value <= max;

        private readonly int[] _beginningMaxMazeDimensions = new int[3] { 8, 8, 12 };
        private readonly int[] _endingMaxMazeDimensions = new int[3] { 20, 23, 25 };

        private bool _customDimensionsEnabled = false;
        public bool CustomDimensionsEnabled
        {
            get => _customDimensionsEnabled;
            set
            {
                _customDimensionsEnabled = value;
                OnPropertyChanged(nameof(CustomDimensionsEnabled));
            }
        }

        public bool CanGenerateSet => !HasErrors;

        public ICommand Generate { get; }

        public ExamSetGeneratorViewModel(ExamSetCreator examSetCreator,
                                         ICurrentStaffMemberStore currentStaffMemberStore,
                                         SelectedModelDetailsStore<ExamSet> examSetDetailsStore,
                                         NavigationService<ExamSetGeneratorResultViewModel> examSetResultNavigationService)
        {
            SetStartSelection();
            Generate = new GenerateExamSetCommand(this, examSetCreator, currentStaffMemberStore, examSetDetailsStore, examSetResultNavigationService);

            PropertyChanged += HandlePropertyChange;
        }

        private void SetStartSelection()
        {
            this.SelectedDifficulty = ExamSetDifficulties.Where(d => d.Value == ExamSetDifficulty.Standard).First();
        }

        protected override void Dispose()
        {
            PropertyChanged -= HandlePropertyChange;
            base.Dispose();
        }

        public void HandlePropertyChange(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedDifficulty))
            {
                HandleDifficultyChange();
            }
        }

        private void HandleDifficultyChange()
        {
            if (SelectedDifficulty.Value == ExamSetDifficulty.Custom)
            {
                CustomDimensionsEnabled = true;
            }
            else
            {
                CustomDimensionsEnabled = false;
                int difficultyIndex = ExamSetDifficulties.ToList().FindIndex(d => d.Value == SelectedDifficulty.Value);
                BeginningMaxMazeDimension = _beginningMaxMazeDimensions[difficultyIndex].ToString();
                EndingMaxMazeDimension = _endingMaxMazeDimensions[difficultyIndex].ToString();
            }
        }
    }
}