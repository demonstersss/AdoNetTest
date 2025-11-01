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
    public partial class RemoveCustomerWindow : Window
    {
        public RemoveCustomerWindow()
        {
            InitializeComponent();
            RefreshCustomers();
        }

        private void RefreshCustomers()
        {
            using (var context = new ApplicationContext())
            {
                var customers = context.Customers.ToList();
                CustomersDataGrid.ItemsSource = customers;
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshCustomers();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer selectedCustomer)
            {
                var result = MessageBox.Show($"Удалить покупателя '{selectedCustomer.Name}' для книги (ID: {selectedCustomer.BookId})?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (var context = new ApplicationContext())
                        {
                            var customerToDelete = context.Customers.FirstOrDefault(c => c.BookId == selectedCustomer.BookId && c.Name == selectedCustomer.Name);
                            if (customerToDelete != null)
                            {
                                context.Customers.Remove(customerToDelete);

                                var book = context.Books.Find(selectedCustomer.BookId);
                                if (book != null)
                                {
                                    book.Count += 1;
                                }

                                context.SaveChanges();
                            }
                        }

                        MessageBox.Show("Покупатель удалён! Count книги увеличен.");
                        RefreshCustomers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите покупателя для удаления!");
            }
        }
    }
}
