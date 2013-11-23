// FFXIVAPP.Plugin.Log
// ShellViewModel.cs
// 
// Copyright © 2013 ZAM Network LLC

#region Usings

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FFXIVAPP.Plugin.Log.Properties;
using FFXIVAPP.Plugin.Log.Views;

#endregion

namespace FFXIVAPP.Plugin.Log
{
    public sealed class ShellViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static ShellViewModel _instance;

        public static ShellViewModel Instance
        {
            get { return _instance ?? (_instance = new ShellViewModel()); }
        }

        #endregion

        #region Declarations

        #endregion

        public ShellViewModel()
        {
            Initializer.LoadSettings();
            Initializer.LoadTabs();
        }

        internal static void Loaded(object sender, RoutedEventArgs e)
        {
            Initializer.ApplyTheming();
            MainView.View.MainViewTC.SelectedIndex = Settings.Default.EnableAll ? 0 : 1;
            Settings.Default.PropertyChanged += DefaultOnPropertyChanged;
        }

        private static void DefaultOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "EnableAll":
                    try
                    {
                        if (MainView.View.MainViewTC.SelectedIndex == 0 && !Settings.Default.EnableAll)
                        {
                            MainView.View.MainViewTC.SelectedIndex = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
                case "EnableDebug":
                    try
                    {
                        if (MainView.View.MainViewTC.SelectedIndex == 2 && !Settings.Default.EnableDebug)
                        {
                            MainView.View.MainViewTC.SelectedIndex = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
            }
        }

        #region Loading Functions

        #endregion

        #region Utility Functions

        #endregion

        #region Command Bindings

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
