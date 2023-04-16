using Diary.Models.Domains;
using Diary.Models.Wrappers;
using Diary.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for SQLSettingsView.xaml
    /// </summary>
    public partial class SQLSettingsView : MetroWindow
    {
        public SQLSettingsView(SQLSettings sqlSettings = null)
        {
            InitializeComponent();
            DataContext = new SQLSettingsViewModel(sqlSettings);
        }
    }
}
