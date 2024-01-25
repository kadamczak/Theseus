using System.Windows;
using System;
using Theseus.WPF.Code.Bases;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Theseus.WPF.Code.Commands.SettingsCommands
{
    /// <summary>
    /// The <c>LoadStringResourcesCommand</c> class changes used string resource file.
    /// </summary>
    public class LoadStringResourcesCommand : CommandBase
    {
        private readonly ResourceDictionary _applicationResources;
        private const string StringFolderPath = @"..\..\Resources\Strings";
        private readonly Regex _stringResourceRegex = new Regex(@$"StringResources\..+\.xaml$");

        public LoadStringResourcesCommand()
        {
            _applicationResources = Application.Current.Resources;
        }

        public override void Execute(object? parameter = null)
        {
            RemovePreviousStringResourceDictionary();
            ResourceDictionary stringResourceDictionary = SelectStringResourceDictionary();

            _applicationResources.MergedDictionaries.Add(stringResourceDictionary);
        }

        private void RemovePreviousStringResourceDictionary()
        {
            var stringResourceDictionary = _applicationResources.MergedDictionaries.FirstOrDefault(d => _stringResourceRegex.IsMatch(d.Source.OriginalString));

            if (stringResourceDictionary is null)
                return;

            _applicationResources.Remove(stringResourceDictionary);
        }

        private ResourceDictionary SelectStringResourceDictionary()
        {
            string chosenLanguage = Properties.Settings.Default.AppLanguage;
            string fileName = @$"{StringFolderPath}\StringResources.{chosenLanguage}.xaml";
            return new ResourceDictionary() { Source = new Uri(fileName, UriKind.Relative) };
        }
    }
}