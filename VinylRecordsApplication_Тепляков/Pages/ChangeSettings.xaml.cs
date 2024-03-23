using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinylRecordsApplication_Тепляков.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeSettings.xaml
    /// </summary>
    public partial class ChangeSettings : Page
    {
        public static SqlConnection Connect;
        public static bool ConnectIsApply = false;
        public ChangeSettings()
        {
            InitializeComponent();
            KeyDown += newSettings;
        }

        private void newSettings(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (newServer.Text == "")
                {
                    MessageBox.Show("Введите название сервера!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (newNameDB.Text == "")
                {
                    MessageBox.Show("Введите название базы данных!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (newLogin.Text == "")
                {
                    MessageBox.Show("Введите логин!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (newPassword.Password == "")
                {
                    MessageBox.Show("Введите пароль!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (newServer.Text != "" && newNameDB.Text != "" && newLogin.Text != "" && newPassword.Password != "")
                {
                    try
                    {
                        Connect = new SqlConnection($@"Server={newServer.Text};Trusted_Connection=No;DataBase={newNameDB.Text};user={newLogin.Text};pwd={newPassword.Password}");
                        Connect.Open();
                        ConnectIsApply = true;
                        MainWindow.mainWindow.Menu.Visibility = Visibility.Visible;
                        MainWindow.mainWindow.OpenPage(new Pages.Records.Main());
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ошибка подключения: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        Connect.Close();
                    }
                }                    
            }
        }
    }
}
