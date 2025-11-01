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
using WpfApp5.DLCWindows;
namespace WpfApp5.WindowsClasses
{

    public partial class ListWindow : Window
    {
        public ListWindow()
        {
            InitializeComponent();
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow("Day");
            reportWindow.Show();
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow("Week");
            reportWindow.Show();
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow("Month");
            reportWindow.Show();
        }

        private void YearButton_Click(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow("Year");
            reportWindow.Show();
        }
    }
}
