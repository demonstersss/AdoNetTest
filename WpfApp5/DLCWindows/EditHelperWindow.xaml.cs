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
    public partial class EditHelperWindow : Window
    {
        private readonly Book _bookToEdit;

        public EditHelperWindow(Book bookToEdit)
        {
            InitializeComponent();
            _bookToEdit = bookToEdit;
            LoadBookData();
        }

        private void LoadBookData()
        {
            TitleTextBox.Text = _bookToEdit.Title ?? string.Empty;
            AuthorIdTextBox.Text = _bookToEdit.AuthorId.ToString();
            PublisherIdTextBox.Text = _bookToEdit.PublisherId.ToString();
            PagesTextBox.Text = _bookToEdit.Pages.ToString();
            GenreIdTextBox.Text = _bookToEdit.GenreId.ToString();
            ReleaseDatePicker.SelectedDate = _bookToEdit.ReleaseDate;
            OriginalPriceTextBox.Text = _bookToEdit.OriginalPrice.ToString();
            TotalPriceTextBox.Text = _bookToEdit.TotalPrice.ToString();
            PreviousBookIdTextBox.Text = _bookToEdit.PreviousBookId?.ToString() ?? string.Empty;
            NextBookIdTextBox.Text = _bookToEdit.NextBookId?.ToString() ?? string.Empty;
            CountTextBox.Text = _bookToEdit.Count.ToString();
            TotalMoneyTextBox.Text = _bookToEdit.TotalMoney.ToString();
        }

        private void SaveBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                string.IsNullOrWhiteSpace(AuthorIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(PublisherIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(PagesTextBox.Text) ||
                string.IsNullOrWhiteSpace(GenreIdTextBox.Text) ||
                ReleaseDatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(OriginalPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(TotalPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(CountTextBox.Text) ||
                string.IsNullOrWhiteSpace(TotalMoneyTextBox.Text) ||
                int.Parse(CountTextBox.Text)<0)


            {
                MessageBox.Show("Заполните все обязательные поля!");
                return;
            }

            try
            {
                using (var context = new ApplicationContext())
                {
                    var book = context.Books.Find(_bookToEdit.Id);
                    if (book != null)
                    {
                        book.Title = TitleTextBox.Text;
                        book.AuthorId = int.Parse(AuthorIdTextBox.Text);
                        book.PublisherId = int.Parse(PublisherIdTextBox.Text);
                        book.Pages = int.Parse(PagesTextBox.Text);
                        book.GenreId = int.Parse(GenreIdTextBox.Text);
                        book.ReleaseDate = ReleaseDatePicker.SelectedDate.Value;
                        book.OriginalPrice = int.Parse(OriginalPriceTextBox.Text);
                        book.TotalPrice = int.Parse(TotalPriceTextBox.Text);
                        book.PreviousBookId = !string.IsNullOrWhiteSpace(PreviousBookIdTextBox.Text) ? int.Parse(PreviousBookIdTextBox.Text) : null;
                        book.NextBookId = !string.IsNullOrWhiteSpace(NextBookIdTextBox.Text) ? int.Parse(NextBookIdTextBox.Text) : null;
                        book.Count = int.Parse(CountTextBox.Text);
                        book.TotalMoney = int.Parse(TotalMoneyTextBox.Text);

                        if (book.Count == 0)
                        {
                            book.IsCribbed = true;
                        }

                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Книга обновлена!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
}
