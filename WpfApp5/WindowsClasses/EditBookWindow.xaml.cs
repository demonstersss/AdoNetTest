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
using WpfApp5.DLCWindows;

namespace WpfApp5.WindowsClasses
{ 
    public partial class EditBookWindow : Window
    {
        public EditBookWindow()
        {
            InitializeComponent();
            RefreshBooks();
        }

        private void RefreshBooks()
        {
            using (var context = new ApplicationContext())
            {
                var books = context.Books.ToList();
                BooksDataGrid.ItemsSource = books;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshBooks();
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                var helperWindow = new EditHelperWindow(selectedBook);
                helperWindow.ShowDialog();
                RefreshBooks(); 
            }
            else
            {
                MessageBox.Show("Выберите книгу для редактирования!");
            }
        }
    }
}
