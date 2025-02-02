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
using WpfApp3.Entities;
namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для InfoWIndow.xaml
    /// </summary>
    public partial class InfoWIndow : Window
    {
        private Window _parentWindow;

        public InfoWIndow(Window parentWindow)
        {
            InitializeComponent();
            LoadData();
            _parentWindow = parentWindow;
        }

        private void LoadData()
        {
            var products = App.Context.Products
               .Select(p => new
               {
                   p.ProductsId,
                   p.НаименованиеПродукции,
                   p.Артикул,
                   p.МинимальнаяСтоимостьДляПартнера
               })
               .ToList();
            ProductsGrid.ItemsSource = products;

            var orders = App.Context.Orders
              .Select(o => new
              {
                  o.OrderId,
                  o.ProductId,
                  o.PartnerId,
                  o.Quantity,
                  o.OrderDate
              })
              .ToList();
            OrdersGrid.ItemsSource = orders;
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите заявку для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedOrder = OrdersGrid.SelectedItem;
            int orderId = (int)selectedOrder.GetType().GetProperty("OrderId").GetValue(selectedOrder, null);

            var orderToDelete = App.Context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (orderToDelete != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить заявку?", "Подтверждение",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Context.Orders.Remove(orderToDelete);
                    App.Context.SaveChanges();
                    LoadData(); // Перезагрузка данных
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            
            _parentWindow.Show();
            this.Close();
        }

    }
}
