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

namespace WpfApp5.WindowsClasses
{
    public partial class DelBookWindow : Window
    {
        public DelBookWindow()
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

        private void DelBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                var result = MessageBox.Show($"Удалить книгу '{selectedBook.Title}' (ID: {selectedBook.Id})?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var context = new ApplicationContext())
                        {
                            var bookToDelete = context.Books.Find(selectedBook.Id);
                            if (bookToDelete != null)
                            {
                                context.Books.Remove(bookToDelete);
                                context.SaveChanges();
                            }
                        }

                        MessageBox.Show("Книга удалена!");
                        RefreshBooks();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для удаления!");
            }
        }
    }
}
