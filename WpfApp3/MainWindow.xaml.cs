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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenInfoWindow(object sender, RoutedEventArgs e)
        {
            InfoWIndow infoWindow = new InfoWIndow(this); // Передаём текущее окно
            infoWindow.Show();
            this.Hide(); // Скрываем главное окно
        }

        private void OpenOrderWindow(object sender, RoutedEventArgs e)
        {
            OrderWndow orderWindow = new OrderWndow(this); // Передаём текущее окно
            orderWindow.Show();
            this.Hide(); // Скрываем главное окно
        }
    }
}
