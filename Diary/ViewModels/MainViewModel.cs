using Diary.Commands;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using Diary.Properties;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public MainViewModel(IDialogCoordinator instance)
        {
            //First query in order to create Database if not exists
            //using (var context = new ApplicationDBContext())
            //{
            //    var students = context.Students.ToList();
            //}

            AddStudentCommand = new RelayCommand(AddEditStudents);
            EditStudentCommand = new RelayCommand(AddEditStudents, CanEditDeleteStudents);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudents, CanEditDeleteStudents);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            SQLSettingsCommand = new RelayCommand(AddEditSQLSettings);
            SplashScreenWindowCommand = new RelayCommand(SplashScreenAtStartup);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);

            _sqlConnectionHelper.IsSQLConnectionSuccessful();
            _dialogCoordinator = instance;

            //RunSplashScreenAtStartup();
            //SplashScreenAtStartup(null);
        }


        public ICommand LoadedWindowCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand SQLSettingsCommand { get; set; }
        public ICommand SplashScreenWindowCommand { get; set; }

        private SQLConnectionHelper _sqlConnectionHelper = new SQLConnectionHelper();
        private StudentWrapper _selectedStudent;
        private IDialogCoordinator _dialogCoordinator;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        private bool CanEditDeleteStudents(object obj)
        {
            return SelectedStudent != null;
        }
        private async Task DeleteStudents(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName}  {SelectedStudent.LastName}?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }

        private void AddEditSQLSettings(object obj)
        {
            var addEditSQLSettingsWindow = new SQLSettingsView(true);
            addEditSQLSettingsWindow.Closed += AddEditSQLSettingsWindow_Closed;
            addEditSQLSettingsWindow.ShowDialog();
        }

        private void AddEditSQLSettingsWindow_Closed(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        private void AddEditStudents(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }


        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszyscy" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }

        public void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>
            (_repository.GetStudents(SelectedGroupId));
        }

        private void RunSplashScreenAtStartup()
        {
            //Uri uri = new Uri("Views/SplashScreenMain.xaml", UriKind.Relative);
            //StreamResourceInfo info = Application.GetResourceStream(uri);
            //XamlReader reader = new XamlReader();
            //Page page = (Page)reader.LoadAsync(info.Stream);
            //this.pageFrame.Content = page;

        }

        private async void SplashScreenAtStartup(object arg)
        {
            //var splashScreen = new SplashScreenMain();
            //splashScreen.ShowDialog();
            //await Task.Delay(3000);
            //splashScreen.Close();
        }


        private async void LoadedWindow(object obj)
        {
            ProgressDialogController controller = await _dialogCoordinator.ShowProgressAsync(this, "Cierpliwości!", "Oczekiwanie na połączenie z bazą danych SQL");

            controller.SetIndeterminate();

            await Task.Run(() =>
            {

                if (!_sqlConnectionHelper.IsSQLConnectionSuccessful())
                    EditSQLConnectionDataDialogCoord();
                else
                {
                    RefreshDiary();
                    InitGroups();
                }

            });

            await controller.CloseAsync();
        }

        public async void EditSQLConnectionDataDialogCoord()
        {
            var dialog = await _dialogCoordinator.ShowMessageAsync(this, "Niewłaściwe dane do połączenia z bazą SQL", "Czy chcesz edytować dane do połączenia z bazą SQL?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog == MessageDialogResult.Affirmative)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    var addEditSQLSettingsWindow = new SQLSettingsView(false);
                    addEditSQLSettingsWindow.ShowDialog();
                });

            }
            else
                Application.Current.Shutdown();
        }
    }
}
