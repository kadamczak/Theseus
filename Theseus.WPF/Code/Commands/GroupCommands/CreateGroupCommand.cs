﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.CommandInterfaces.GroupCommandInterfaces;
using Theseus.Domain.Models.GroupRelated;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.GroupCommands
{
    public class CreateGroupCommand : AsyncCommandBase
    {
        private readonly StaffMemberGroupsViewModel _staffMemberGroupsViewModel;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly ICreateGroupCommand _createGroupCommand;

        public CreateGroupCommand(StaffMemberGroupsViewModel staffMemberGroupsViewModel,
                                  ICurrentStaffMemberStore currentStaffMemberStore,
                                  ICreateGroupCommand createGroupCommand)
        {
            _staffMemberGroupsViewModel = staffMemberGroupsViewModel;
            _currentStaffMemberStore = currentStaffMemberStore;
            _createGroupCommand = createGroupCommand;

            _staffMemberGroupsViewModel.PropertyChanged += OnPropertyChanged;
        }

        protected override void Dispose()
        {
            _staffMemberGroupsViewModel.PropertyChanged -= OnPropertyChanged;
            base.Dispose();
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Group group = new Group()
            {
                Id = Guid.NewGuid(),
                Name = _staffMemberGroupsViewModel.GroupName,
                Owner = _currentStaffMemberStore.StaffMember ?? throw new StaffMemberNotLoggedInException(),
                StaffMembers = new List<StaffMember>() { _currentStaffMemberStore.StaffMember }
            };

            _staffMemberGroupsViewModel.GroupName = string.Empty;
            await _createGroupCommand.CreateGroup(group);

            _staffMemberGroupsViewModel.ShowDetailsGroupCommandListViewModel.AddModelToActionableModels(group);
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_staffMemberGroupsViewModel.CanCreate))
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _staffMemberGroupsViewModel.CanCreate && base.CanExecute(parameter);
        }
    }
}