using Diary.Commands;
using Diary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RefreshStudentsCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);
            Students = new ObservableCollection<Student>
            {
                new Student
                {
                    FirstName="Jakub",
                    LastName="Zięba",
                    Group=new Group{Id=1,Name="6B" }
                },
                new Student
                {
                    FirstName="Jan",
                    LastName="Zięba",
                    Group=new Group{Id=2,Name="2B" }
                },
                new Student
                {
                    FirstName="Zofia",
                    LastName="Zięba",
                    Group=new Group{Id=3,Name="1B" }
                },
            };
        }

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


        private void RefreshStudents(object obj)
        {

        }
        private bool CanRefreshStudents(object obj)
        {
            return true;
        }

    }
}
