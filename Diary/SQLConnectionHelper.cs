using Diary.Views;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using Diary.Properties;

namespace Diary
{
    public class SQLConnectionHelper
    {
        public async void IsSQLConnectionSuccessful()
        {


            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
            }
            catch (SqlException ex)
            {
                await EditSQLConnectionDataAsync();
            }
        }

        public void TestSQLConnection()
        {
            var context = new ApplicationDBContext();

            try
            {
                using (var connection = context.Database.Connection)
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
            }
            catch (SqlException ex)
            {
                EditSQLConnectionData();
            }
            SQLConnectionSuccessMsg();
        }

        private async Task EditSQLConnectionDataAsync()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Niewłaściwe dane do połączenia z bazą SQL", "Czy chcesz edytować dane do połączenia z bazą SQL?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog == MessageDialogResult.Affirmative)
            {
                var addEditSQLSettingsWindow = new SQLSettingsView(false);
                addEditSQLSettingsWindow.ShowDialog();
            }
            else
                Application.Current.Shutdown();
        }

        private void EditSQLConnectionData()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = metroWindow.ShowModalMessageExternal("Niewłaściwe dane do połączenia z bazą SQL", "Czy chcesz edytować dane do połączenia z bazą SQL?", MessageDialogStyle.AffirmativeAndNegative);
            if (dialog == MessageDialogResult.Affirmative)
            {
                var addEditSQLSettingsWindow = new SQLSettingsView(true);
                addEditSQLSettingsWindow.ShowDialog();
            }
            else
                Application.Current.MainWindow.Close();
        }

        private void SQLConnectionSuccessMsg()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = metroWindow.ShowModalMessageExternal("Połaczenie z bazą danych SQL", $"Test połączenia z bazą danych {Settings.Default.SQLDatabaseName} przebiegł pomyślnie.", MessageDialogStyle.Affirmative);
        }
    }
}
