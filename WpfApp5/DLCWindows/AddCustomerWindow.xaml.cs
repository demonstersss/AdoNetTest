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
    public partial class AddCustomerWindow : Window
    {
        private readonly Book _selectedBook;

        public AddCustomerWindow(Book selectedBook)
        {
            InitializeComponent();
            _selectedBook = selectedBook;
            RefreshCustomers();
        }

        private void RefreshCustomers()
        {
            using (var context = new ApplicationContext())
            {
                var customers = context.Customers.Where(c => c.BookId == _selectedBook.Id).ToList();
                CustomersDataGrid.ItemsSource = customers;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CustomerNameTextBox.Text))
            {
                MessageBox.Show("Введите имя покупателя!");
                return;
            }

            try
            {
                using (var context = new ApplicationContext())
                {
                    var book = context.Books.Find(_selectedBook.Id);
                    if (book == null || book.Count <= 0)
                    {
                        MessageBox.Show("Недостаточно экземпляров книги (Count <= 0)!");
                        return;
                    }

                    var customer = new Customer
                    {
                        Name = CustomerNameTextBox.Text,
                        BookId = _selectedBook.Id
                    };
                    context.Customers.Add(customer);

                    book.Count -= 1;

                    context.SaveChanges();
                }

                MessageBox.Show($"Книга '{_selectedBook.Title}' назначена покупателю '{CustomerNameTextBox.Text}'! Count уменьшен.");
                CustomerNameTextBox.Clear();
                RefreshCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
}
