using Microsoft.EntityFrameworkCore;
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

    public partial class SellBookWindow : Window
    {
        public SellBookWindow()
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

        private void SellBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook && selectedBook.Count > 0)
            {
                try
                {
                    using (var context = new ApplicationContext())
                    {
                        var book = context.Books 
                            .FirstOrDefault(b => b.Id == selectedBook.Id);

                        if (book == null)
                        {
                            MessageBox.Show("Книга не найдена в БД!");
                            RefreshBooks();
                            return;
                        }

                        if (book.Count <= 0)
                        {
                            MessageBox.Show("Недостаточно экземпляров (Count <= 0)!");
                            RefreshBooks();
                            return;
                        }

                        book.Count -= 1;
                        book.TotalMoney += book.TotalPrice;

                        if (book.Count == 0)
                        {
                            book.IsCribbed = true;
                        }

                        var purchase = new Purchase
                        {
                            BookId = book.Id,
                            PurchaseDate = DateTime.Now
                        };
                        context.Purchases.Add(purchase);

                        context.SaveChanges();
                    }

                    MessageBox.Show($"Книга '{selectedBook.Title}' (ID: {selectedBook.Id}) успешно продана!\nНовый Count: {selectedBook.Count - 1}\nTotalMoney: +{selectedBook.TotalPrice}");
                    RefreshBooks();
                }
                catch (DbUpdateException dbEx)
                {
                    MessageBox.Show($"Ошибка обновления БД (FK/конфликт): {dbEx.InnerException?.Message ?? dbEx.Message}\nПроверьте ID ссылок (AuthorId, PublisherId и т.д.).");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Общая ошибка при продаже: {ex.Message}\nStackTrace: {ex.StackTrace}");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу с Count > 0!");
            }
        }
    }
}
