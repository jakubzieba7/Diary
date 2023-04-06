using Diary.Commands;
using Diary.Models;
using Diary.Models.Wrappers;
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
            using (var context = new ApplicationDBContext())
            {
                var students = context.Students.ToList();
            }

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


        private StudentWrapper _selectedStudent;

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

        private ObservableCollection<GroupWrapper> _groups;

        public ObservableCollection<GroupWrapper> Groups
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
            Groups = new ObservableCollection<GroupWrapper>
            {
            new GroupWrapper{ Id=0,Name="Wszyscy"},
            new GroupWrapper{ Id=1,Name="1A"},
            new GroupWrapper{ Id=2,Name="1B"},
            new GroupWrapper{ Id=3,Name="2A"},
            new GroupWrapper{ Id=4,Name="2B"},
            new GroupWrapper{ Id=5,Name="3A"},
            new GroupWrapper{ Id=6,Name="3B"},
            new GroupWrapper{ Id=7,Name="4A"},
            new GroupWrapper{ Id=8,Name="4B"},
            new GroupWrapper{ Id=9,Name="5A"},
            new GroupWrapper{ Id=10,Name="5B"},
            new GroupWrapper{ Id=11,Name="6A"},
            new GroupWrapper{ Id=12,Name="6B"}
            };

            SelectedGroupId = 0;
        }

        public void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>
            {
                new StudentWrapper
                {
                    FirstName="Jakub",
                    LastName="Zięba",
                    Group=new GroupWrapper{Id=12,Name="6B" }
                },
                new StudentWrapper
                {
                    FirstName="Jan",
                    LastName="Zięba",
                    Group=new GroupWrapper{Id=4,Name="2B" }
                },
                new StudentWrapper
                {
                    FirstName="Zofia",
                    LastName="Zięba",
                    Group=new GroupWrapper{Id=2,Name="1B" }
                },
            };
        }
    }
}
