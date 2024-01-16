using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Theseus.WPF.Code.Bases
{
    /// <summary>
    /// The <c>ErrorCheckingViewModel</c> abstract class contains common <c>INotifyDataErrorInfo</c> implementation for <c>ViewModelBase</c> subclasses.
    /// Helps with instant error checking in GUI.
    /// </summary>
    public abstract class ErrorCheckingViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary = new();

        public bool HasErrors => _propertyNameToErrorsDictionary.Any();
        public IEnumerable GetErrors(string? propertyName) => _propertyNameToErrorsDictionary!.GetValueOrDefault(propertyName, new List<string>());

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        protected void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        protected void AddError(string propertyName, string errorMessage)
        {
            AddEmptyListIfNoKey(propertyName);
            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        private void AddEmptyListIfNoKey(string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
        }

        protected void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        protected void ClearError(string propertyName, string errorMessage)
        {
            _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName)?.Remove(errorMessage);
            OnErrorsChanged(propertyName);
        }
    }
}