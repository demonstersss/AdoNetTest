using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp5.DLCWindows
{
    public partial class NameSearchWindow : Window
    {
        public NameSearchWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = NameTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Введите текст для поиска!");
                return;
            }

            using (var context = new ApplicationContext())
            {
                var books = context.Books
                    .Where(b => b.Title != null && b.Title.Contains(searchText))
                    .ToList();
                BooksDataGrid.ItemsSource = books;
            }
        }
    }
}
