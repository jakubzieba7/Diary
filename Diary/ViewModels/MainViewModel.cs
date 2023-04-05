using Diary.Commands;
using Diary.Models;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudents);
            EditStudentCommand = new RelayCommand(AddEditStudents, CanEditDeleteStudents);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudents, CanEditDeleteStudents);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);

            RefreshDiary();
            InitGroups();
        }

        

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }


        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
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
                OnPropertyChanged(nameof(Groups));
            }
        }

        private bool CanEditDeleteStudents(object obj)
        {
            return SelectedStudent != null;
        }
        private async Task DeleteStudents(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName}  {SelectedStudent.LastName}",MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            RefreshDiary();
        }

        private void AddEditStudents(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as Student);
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
            Groups = new ObservableCollection<Group>
            {
            new Group{ Id=0,Name="Wszyscy"},
            new Group{ Id=1,Name="1A"},
            new Group{ Id=2,Name="1B"},
            new Group{ Id=3,Name="2A"},
            new Group{ Id=4,Name="2B"},
            new Group{ Id=5,Name="3A"},
            new Group{ Id=6,Name="3B"},
            new Group{ Id=7,Name="4A"},
            new Group{ Id=8,Name="4B"},
            new Group{ Id=9,Name="5A"},
            new Group{ Id=10,Name="5B"},
            new Group{ Id=11,Name="6A"},
            new Group{ Id=12,Name="6B"}
            };

            SelectedGroupId = 0;
        }

        public void RefreshDiary()
        {
            Students = new ObservableCollection<Student>
            {
                new Student
                {
                    FirstName="Jakub",
                    LastName="Zięba",
                    Group=new Group{Id=12,Name="6B" }
                },
                new Student
                {
                    FirstName="Jan",
                    LastName="Zięba",
                    Group=new Group{Id=4,Name="2B" }
                },
                new Student
                {
                    FirstName="Zofia",
                    LastName="Zięba",
                    Group=new Group{Id=2,Name="1B" }
                },
            };
        }
    }
}
