using System;
using System.Collections.Specialized;
using System.Linq;
using Theseus.Domain.CommandInterfaces;
using Theseus.Domain.Models.SetRelated;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.SetCommands
{
    public class CreateSetManuallyCommand : CommandBase
    {
        private readonly AddToSetMazeCommandListViewModel _addToSetMazeCommandListViewModel;
        private readonly ICreateExamSetCommand _createExamSetCommand;
        private readonly NavigationService<CreateSetViewModel> _createSetNavigationService;

        public CreateSetManuallyCommand(AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel,
                                        ICreateExamSetCommand createExamSetCommand,
                                        NavigationService<CreateSetViewModel> createSetNavigationService)
        {
            _addToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
            _createExamSetCommand = createExamSetCommand;
            _createSetNavigationService = createSetNavigationService;
            _addToSetMazeCommandListViewModel.SelectedMazes.CollectionChanged += OnCollectionChanged;
        }

        protected override void Dispose()
        {
            _addToSetMazeCommandListViewModel.SelectedMazes.CollectionChanged -= OnCollectionChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            var selectedMazes = _addToSetMazeCommandListViewModel.SelectedMazes.ToList();
            ExamSet examSet = new ExamSet(Guid.NewGuid(), selectedMazes);

            _createExamSetCommand.CreateExamSet(examSet);

            _createSetNavigationService.Navigate();
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _addToSetMazeCommandListViewModel.SelectedMazes.Any() && base.CanExecute(parameter);
        }
    }
}