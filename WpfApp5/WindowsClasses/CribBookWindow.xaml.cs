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
    public partial class CribBookWindow : Window
    {
        public CribBookWindow()
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

        private void CribBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                if (selectedBook.Count <= 0)
                {
                    MessageBox.Show("Нельзя изменить IsCribbed, если Count <= 0!");
                    return;
                }

                try
                {
                    using (var context = new ApplicationContext())
                    {
                        var book = context.Books.FirstOrDefault(b => b.Id == selectedBook.Id);

                        if (book == null)
                        {
                            MessageBox.Show("Книга не найдена в БД!");
                            RefreshBooks();
                            return;
                        }

                        if (book.Count <= 0)
                        {
                            MessageBox.Show("Нельзя изменить IsCribbed, если Count <= 0!");
                            RefreshBooks();
                            return;
                        }

                        book.IsCribbed = !book.IsCribbed;

                        context.SaveChanges();
                    }

                    MessageBox.Show($"Статус IsCribbed для книги '{selectedBook.Title}' (ID: {selectedBook.Id}) изменён на {!selectedBook.IsCribbed}!");
                    RefreshBooks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при изменении: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для crib!");
            }
        }
    }
}
