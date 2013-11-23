﻿// FFXIVAPP.Plugin.Log
// MainViewModel.cs
// 
// Copyright © 2013 ZAM Network LLC

#region Usings

using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using FFXIVAPP.Common.RegularExpressions;
using FFXIVAPP.Common.ViewModelBase;
using FFXIVAPP.Plugin.Log.Properties;
using FFXIVAPP.Plugin.Log.Utilities;
using FFXIVAPP.Plugin.Log.Views;

#endregion

namespace FFXIVAPP.Plugin.Log.ViewModels
{
    internal sealed class MainViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static MainViewModel _instance;

        public static MainViewModel Instance
        {
            get { return _instance ?? (_instance = new MainViewModel()); }
        }

        #endregion

        #region Declarations

        public static readonly Regex TranalateIsValid = new Regex(@"^/\w( \w+ \w+)?$", SharedRegEx.DefaultOptions);
        public ICommand DeleteTabCommand { get; private set; }
        public ICommand ManualTranslateCommand { get; private set; }

        #endregion

        public MainViewModel()
        {
            DeleteTabCommand = new DelegateCommand(DeleteTab);
            ManualTranslateCommand = new DelegateCommand<string>(ManualTranslate);
        }

        #region Loading Functions

        #endregion

        #region Utility Functions

        #endregion

        #region Command Bindings

        /// <summary>
        /// </summary>
        private static void DeleteTab()
        {
            if (MainView.View.MainViewTC.SelectedIndex < 3)
            {
                return;
            }
            var selection = Settings.Default.EnableDebug ? 2 : 1;
            PluginViewModel.Instance.Tabs.RemoveAt(MainView.View.MainViewTC.SelectedIndex - 3);
            if (PluginViewModel.Instance.Tabs.Any())
            {
                return;
            }
            MainView.View.MainViewTC.SelectedIndex = selection;
        }

        /// <summary>
        /// </summary>
        /// <param name="value"> </param>
        private static void ManualTranslate(string value)
        {
            value = value.Trim();
            var outLang = GoogleTranslate.Offsets[Settings.Default.ManualTranslate].ToString();
            if (value.Length <= 0)
            {
                return;
            }
            var tmpTranString = GoogleTranslate.TranslateText(value, "en", outLang, false);
            MainView.View.Chatter.Text = tmpTranString;
            if (!Settings.Default.SendToGame)
            {
                return;
            }
            var chatMode = MainView.View.CM.Text.Trim();
            var match = TranalateIsValid.Match(chatMode);
            if (!match.Success)
            {
                return;
            }
            var command = String.Format("{0} {1}", chatMode, tmpTranString);
            //Plugin.PHost.Commands(Plugin.PName, new[]
            //{
            //    command
            //});
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        #endregion
    }
}
