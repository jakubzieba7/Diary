using Diary.Commands;
using Diary.Models;
using Diary.Properties;
using Diary.Views;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class SQLSettingsViewModel : ViewModelBase
    {
        public SQLSettingsViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            TestSQLConnectionCommand = new RelayCommand(TestSQLConnection);

            _sqlSettings = new SQLSettings();
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand TestSQLConnectionCommand { get; set; }

        private SQLConnectionHelper _sqlConnectionHelper = new SQLConnectionHelper();
        private SQLSettings _sqlSettings;

        public SQLSettings SQLSettings
        {
            get
            {
                return _sqlSettings;
            }
            set
            {
                _sqlSettings = value;
                OnPropertyChanged();
            }
        }

        private void TestSQLConnection(object obj)
        {
            var addEditSQLSettingsWindow = new SQLSettingsView();

            _sqlConnectionHelper.TestSQLConnection();
            Settings.Default.Save();
            CloseWindow(obj as Window);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }
        
        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void Confirm(object obj)
        {
            if (!SQLSettings.IsValid)
                return;

            Settings.Default.Save();
            AplicationRestart();
        }

        private void AplicationRestart()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
