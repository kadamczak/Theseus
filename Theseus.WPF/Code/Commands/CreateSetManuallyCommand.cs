using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands
{
    public class CreateSetManuallyCommand : CommandBase
    {
        private readonly AddToSetMazeCommandListViewModel _addToSetMazeCommandListViewModel;

        public CreateSetManuallyCommand(AddToSetMazeCommandListViewModel addToSetMazeCommandListViewModel)
        {
            this._addToSetMazeCommandListViewModel = addToSetMazeCommandListViewModel;
            _addToSetMazeCommandListViewModel.SelectedMazes.CollectionChanged += OnCollectionChanged;
        }

        protected override void Dispose()
        {
            _addToSetMazeCommandListViewModel.SelectedMazes.CollectionChanged -= OnCollectionChanged;
            base.Dispose();
        }

        public override void Execute(object? parameter)
        {
            int a = 5;
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