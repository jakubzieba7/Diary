using Diary.Commands;
using Diary.Models;
using Diary.Models.Wrappers;
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
    internal class AddEditStudentViewModel : ViewModelBase
    {
        public AddEditStudentViewModel(StudentWrapper student = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if (student == null)
            {
                Student = new StudentWrapper();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }


        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private StudentWrapper _student;

        public StudentWrapper Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChanged(nameof(Student));
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged($"{nameof(IsUpdate)}");
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
            if (!IsUpdate)
            {
                AddStudent();
            }
            else
                UpdateStudent();


            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            throw new NotImplementedException();
        }

        private void AddStudent()
        {
            throw new NotImplementedException();
        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<GroupWrapper>
            {
            new GroupWrapper{ Id=0,Name="-- brak --"},
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

            Student.Group.Id = 0;
        }
    }
}
