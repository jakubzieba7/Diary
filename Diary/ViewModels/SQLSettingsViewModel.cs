using Diary.Commands;
using Diary.Models;
using Diary.Properties;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class SQLSettingsViewModel : ViewModelBase
    {
        public SQLSettingsViewModel(bool canCloseWindow)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            TestSQLConnectionCommand = new RelayCommand(TestSQLConnection);

            _sqlSettings = new SQLSettings();
            _canCloseWindow = canCloseWindow;
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand TestSQLConnectionCommand { get; set; }

        private SQLConnectionHelper _sqlConnectionHelper = new SQLConnectionHelper();
        private SQLSettings _sqlSettings;
        private readonly bool _canCloseWindow;

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
            Settings.Default.Save();
            _sqlConnectionHelper.TestSQLConnection();
            //CloseWindow(obj as Window);
        }

        private void Close(object obj)
        {
            if (_canCloseWindow)
                CloseWindow(obj as Window);
            else
                Application.Current.Shutdown();
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void Confirm(object obj)
        {
            if (!SQLSettings.IsValid)
                return;

            if (!_sqlConnectionHelper.IsSQLConnectionSuccessful())
                _sqlConnectionHelper.EditSQLConnectionData();
            else
            {
                Settings.Default.Save();
                AplicationRestart();
            }
        }

        private void AplicationRestart()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
