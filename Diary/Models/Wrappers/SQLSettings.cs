using Diary.Properties;
using System.ComponentModel;

namespace Diary.Models.Wrappers
{
    public class SQLSettings : IDataErrorInfo
    {
        //public SQLSettings()
        //{
        //    ServerName = Settings.Default.ServerName;
        //    ServerInstance = Settings.Default.ServerInstance;
        //    SQLDatabaseName = Settings.Default.SQLDatabaseName;
        //    ServerUserName = Settings.Default.ServerUserName;
        //    ServerUserPassword = Settings.Default.ServerUserPassword;
        //}
        public string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        public string ServerInstance
        {
            get
            {
                return Settings.Default.ServerInstance;
            }
            set
            {
                Settings.Default.ServerInstance = value;
            }
        }
        public string SQLDatabaseName
        {
            get
            {
                return Settings.Default.SQLDatabaseName;
            }
            set
            {
                Settings.Default.SQLDatabaseName = value;
            }
        }
        public string ServerUserName
        {
            get
            {
                return Settings.Default.ServerUserName;
            }
            set
            {
                Settings.Default.ServerUserName = value;
            }
        }
        public string ServerUserPassword
        {
            get
            {
                return Settings.Default.ServerUserPassword;
            }
            set
            {
                Settings.Default.ServerUserPassword = value;
            }
        }

        private bool _isServerNameValid;
        private bool _isServerInstanceValid;
        private bool _isSQLDataBaseNameValid;
        private bool _isUserNameValid;
        private bool _isUserPasswordValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Pole Nazwa serwera jest wymagane.";
                            _isServerNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerNameValid = true;
                        }
                        break;
                    case nameof(ServerInstance):
                        if (string.IsNullOrWhiteSpace(ServerInstance))
                        {
                            Error = "Pole Instancja serwera jest wymagane.";
                            _isServerInstanceValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerInstanceValid = true;
                        }
                        break;
                    case nameof(SQLDatabaseName):
                        if (string.IsNullOrWhiteSpace(SQLDatabaseName))
                        {
                            Error = "Pole Nazwa bazy danych SQL jest wymagane.";
                            _isSQLDataBaseNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isSQLDataBaseNameValid = true;
                        }
                        break;
                    case nameof(ServerUserName):
                        if (string.IsNullOrWhiteSpace(ServerUserName))
                        {
                            Error = "Pole Login jest wymagane.";
                            _isUserNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserNameValid = true;
                        }
                        break;
                    case nameof(ServerUserPassword):
                        if (string.IsNullOrWhiteSpace(ServerUserPassword))
                        {
                            Error = "Pole Hasło użytkownika jest wymagane.";
                            _isUserPasswordValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserPasswordValid = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }
        public string Error { get; set; }
        public bool IsValid
        {
            get
            {
                return _isServerNameValid && _isServerInstanceValid && _isSQLDataBaseNameValid && _isUserNameValid && _isUserPasswordValid;
            }
        }
    }
}
