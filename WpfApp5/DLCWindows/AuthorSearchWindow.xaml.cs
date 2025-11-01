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
using WpfApp5.Classes;

namespace WpfApp5.DLCWindows
{ 
    public partial class AuthorSearchWindow : Window
    {
        public AuthorSearchWindow()
        {
            InitializeComponent();
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            using (var context = new ApplicationContext())
            {
                var authors = context.Authors.ToList();
                AuthorsDataGrid.ItemsSource = authors;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorsDataGrid.SelectedItem is Author selectedAuthor)
            {
                var booksWindow = new BooksByAuthorWindow(selectedAuthor.Id);
                booksWindow.Show();
            }
            else
            {
                MessageBox.Show("Выберите автора!");
            }
        }
    }
}
