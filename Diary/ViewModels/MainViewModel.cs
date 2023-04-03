using Diary.Commands;
using System;
using System.Collections.Generic;
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
        }
        public ICommand RefreshStudentsCommand { get; set; }


        private void RefreshStudents(object obj)
        {
            
        }
        private bool CanRefreshStudents(object obj)
        {
            return true;
        }

    }
}
