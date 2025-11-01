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

namespace WpfApp5.Windows
{
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
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

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                string.IsNullOrWhiteSpace(AuthorIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(PublisherIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(PagesTextBox.Text) ||
                string.IsNullOrWhiteSpace(GenreIdTextBox.Text) ||
                ReleaseDatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(OriginalPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(TotalPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(CountTextBox.Text))
            {
                MessageBox.Show("Заполните все обязательные поля!");
                return;
            }

            try
            {
                var book = new Book
                {
                    Title = TitleTextBox.Text,
                    AuthorId = int.Parse(AuthorIdTextBox.Text),
                    PublisherId = int.Parse(PublisherIdTextBox.Text),
                    Pages = int.Parse(PagesTextBox.Text),
                    GenreId = int.Parse(GenreIdTextBox.Text),
                    ReleaseDate = ReleaseDatePicker.SelectedDate.Value,
                    OriginalPrice = int.Parse(OriginalPriceTextBox.Text),
                    TotalPrice = int.Parse(TotalPriceTextBox.Text),
                    PreviousBookId = !string.IsNullOrWhiteSpace(PreviousBookIdTextBox.Text) ? int.Parse(PreviousBookIdTextBox.Text) : null,
                    NextBookId = !string.IsNullOrWhiteSpace(NextBookIdTextBox.Text) ? int.Parse(NextBookIdTextBox.Text) : null,
                    Count = int.Parse(CountTextBox.Text),
                    TotalMoney = 0,
                    IsCribbed = false 
                };

                if (book.Count <= 0)
                {
                    MessageBox.Show("Count должен быть больше 0!");
                    return;
                }

                using (var context = new ApplicationContext())
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                }

                MessageBox.Show("Книга добавлена!");
                RefreshBooks();

                TitleTextBox.Clear();
                AuthorIdTextBox.Clear();
                PublisherIdTextBox.Clear();
                PagesTextBox.Clear();
                GenreIdTextBox.Clear();
                ReleaseDatePicker.SelectedDate = null;
                OriginalPriceTextBox.Clear();
                TotalPriceTextBox.Clear();
                PreviousBookIdTextBox.Clear();
                NextBookIdTextBox.Clear();
                CountTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshBooks();
        }
    }
}
