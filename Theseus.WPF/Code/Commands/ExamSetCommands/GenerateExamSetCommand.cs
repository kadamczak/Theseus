using System.ComponentModel;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels.SetViewModels;

namespace Theseus.WPF.Code.Commands.ExamSetCommands
{
    public class GenerateExamSetCommand : CommandBase
    {
        private readonly ExamSetGeneratorViewModel _examSetGeneratorViewModel;

        public GenerateExamSetCommand(ExamSetGeneratorViewModel examSetGeneratorViewModel)
        {
            _examSetGeneratorViewModel = examSetGeneratorViewModel;

            _examSetGeneratorViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected override void Dispose()
        {
            _examSetGeneratorViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_examSetGeneratorViewModel.CanGenerateSet))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _examSetGeneratorViewModel.CanGenerateSet && base.CanExecute(parameter);
        }
    }
}