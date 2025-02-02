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
    /// Логика взаимодействия для OrderWndow.xaml
    /// </summary>
    public partial class OrderWndow : Window
    {
        private Window _parentWindow;

        public int SelectedPartnerId { get; set; }
        public int SelectedProductId { get; set; }
        public OrderWndow(Window parentWindow)
        {
            InitializeComponent();
            DataContext = this;
            LoadData();
            _parentWindow = parentWindow;
        }

        private void LoadData()
        {
            var products = App.Context.Products.ToList();

            ProductComboBox.ItemsSource = products;
            ProductComboBox.DisplayMemberPath = "НаименованиеПродукции";
            ProductComboBox.SelectedValuePath = "ProductsId";

            var partners = App.Context.Partners
                    .ToList()
                    .Select(p => new
                    {
                        p.PartnersId,
                        Наименование = p.PartnerName != null ? p.PartnerName.Наименование : "Без имени"
                    })
                    .ToList();

                PartnerComboBox.ItemsSource = partners;
                PartnerComboBox.DisplayMemberPath = "Наименование";
                PartnerComboBox.SelectedValuePath = "PartnersId";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.Show(); // Показываем предыдущее окно
            this.Close(); // Закрываем текущее
        }
        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProductId = ProductComboBox.SelectedValue as int? ?? 0;
        }

        private void PartnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedPartnerId = PartnerComboBox.SelectedValue as int? ?? 0;
        }

        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            if (SelectedProductId == 0 || SelectedPartnerId == 0)
            {
                MessageBox.Show("Выберите продукт и партнера.");
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество.");
                return;
            }

                var order = new Order
                {
                    ProductId = SelectedProductId,
                    PartnerId = SelectedPartnerId,
                    Quantity = quantity,
                    OrderDate = DateTime.Now
                };

                App.Context.Orders.Add(order);
                App.Context.SaveChanges();

                MessageBox.Show("Заказ успешно создан.");
            InfoWIndow infoWindow = new InfoWIndow(this);
            infoWindow.Show();
            this.Hide();
        }
    }
}
