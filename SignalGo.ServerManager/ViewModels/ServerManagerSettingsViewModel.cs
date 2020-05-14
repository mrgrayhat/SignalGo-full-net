﻿using System;
using MvvmGo.Commands;
using MvvmGo.ViewModels;
using System.Diagnostics;
using SignalGo.ServerManager.Models;
using System.Windows.Forms;
using System.IO;

namespace SignalGo.ServerManager.ViewModels
{
    public class ServerManagerSettingsViewModel : BaseViewModel
    {
        public Command BrowseBackupPathCommand { get; set; }
        public Command BrowseLoggerPathCommand { get; set; }
        public Command RestoreDefaults { get; set; }
        public Command SaveCommand { get; set; }
        public Command BackCommand { get; set; }
        public ServerManagerSettingsViewModel()
        {
            SaveCommand = new Command(Save);
            BackCommand = new Command(Back);
            BrowseBackupPathCommand = new Command(BrowseBackupPath);
            BrowseLoggerPathCommand = new Command(BrowseLoggerPath);
        }


        private void BrowseBackupPath()
        {
            using FolderBrowserDialog BrowseBackupPathDialog = new FolderBrowserDialog();
            BrowseBackupPathDialog.SelectedPath = BrowseBackupPathDialog.SelectedPath;
            if (BrowseBackupPathDialog.ShowDialog() == DialogResult.OK)
                CurrentUserSettingInfo.UserSettings.BackupPath = BrowseBackupPathDialog.SelectedPath;
        }

        private void BrowseLoggerPath()
        {
            using FolderBrowserDialog BrowseLoggerPathDialog = new FolderBrowserDialog();
            BrowseLoggerPathDialog.SelectedPath = BrowseLoggerPathDialog.SelectedPath;
            if (BrowseLoggerPathDialog.ShowDialog() == DialogResult.OK)
                CurrentUserSettingInfo.UserSettings.LoggerPath = BrowseLoggerPathDialog.SelectedPath;
        }
        public UserSettingInfo CurrentUserSettingInfo
        {
            get
            {
                return UserSettingInfo.Current;
            }
        }

        UserSetting _UserSetting;
        public UserSetting UserSetting
        {
            get
            {
                return _UserSetting;
            }

            set
            {
                _UserSetting = value;
                OnPropertyChanged(nameof(UserSetting));
            }
        }
        
        public void Back()
        {
            MainWindowViewModel.MainFrame.GoBack();
        }
        public void Save()
        {
            try
            {
                UserSettingInfo.SaveUserSettingInfo();
                MainWindowViewModel.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
        }




    }
}