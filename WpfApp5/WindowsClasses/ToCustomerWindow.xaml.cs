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

    public partial class ToCustomerWindow : Window
    {
        public ToCustomerWindow()
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                if (selectedBook.Count <= 0)
                {
                    MessageBox.Show("Недостаточно экземпляров книги (Count <= 0)!");
                    return;
                }

                var addWindow = new AddCustomerWindow(selectedBook);
                addWindow.ShowDialog();
                RefreshBooks(); 
            }
            else
            {
                MessageBox.Show("Выберите книгу для назначения покупателю!");
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var removeWindow = new RemoveCustomerWindow();
            removeWindow.ShowDialog();
            RefreshBooks();
        }
    }
}
