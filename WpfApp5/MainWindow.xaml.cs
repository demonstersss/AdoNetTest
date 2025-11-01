using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp5.Classes;
using WpfApp5.Windows;
using WpfApp5.WindowsClasses;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            InitializeComponent();

        }
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == "52" && PasswordTextBox.Password == "52")
            {
                LoginPanel.Visibility = Visibility.Collapsed;
                ButtonsGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
                LoginTextBox.Clear();
                PasswordTextBox.Clear();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddBookWindow();
            addWindow.Show();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var delWindow = new DelBookWindow();
            delWindow.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditBookWindow();
            editWindow.Show();
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            var sellWindow = new SellBookWindow();
            sellWindow.Show();
        }

        private void CribButton_Click(object sender, RoutedEventArgs e)
        {
            var cribWindow = new CribBookWindow();
            cribWindow.Show();
        }

        private void SaleButton_Click(object sender, RoutedEventArgs e)
        {
            var saleWindow = new SaleGenreWindow();
            saleWindow.Show();
        }

        private void ToCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var toCustomerWindow = new ToCustomerWindow();
            toCustomerWindow.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Show();
        }

        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            var listWindow = new ListWindow();
            listWindow.Show();
        }
    }
}