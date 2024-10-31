using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Facade_Pattern.DeliveryStatusTracker;

namespace Facade_Pattern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DeliveryFacade _deliveryFacade;

        public MainWindow()
        {
            InitializeComponent();
            _deliveryFacade = new DeliveryFacade();
        }

        private void PlaceOrderButton_Click(object sender, RoutedEventArgs e)
        {
            string orderDetails = OrderDetailsTextBox.Text;
            _deliveryFacade.PlaceOrder(orderDetails);
            MessageBox.Show("Заказ размещён.");
        }

        private void CalculateCostButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(DistanceTextBox.Text, out double distance) &&
                double.TryParse(WeightTextBox.Text, out double weight))
            {
                double cost = _deliveryFacade.GetDeliveryCost(distance, weight);
                CostResultTextBlock.Text = $"Стоимость доставки: {cost:C}";
            }
            else
            {
                MessageBox.Show("Введите правильные значения для расстояния и веса.");
            }
        }

        private void CheckStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(OrderIdTextBox.Text, out int orderId))
            {
                DeliveryStatus status = _deliveryFacade.GetOrderStatus(orderId);
                var localizedStatus = _deliveryFacade.GetLocalizedStatus(status).ToString();
                StatusResultTextBlock.Text = $"Статус заказа: {localizedStatus}";
            }
            else
            {
                MessageBox.Show("Введите корректный ID заказа.");
            }
        }

        private void UpdateStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(OrderIdTextBox.Text, out int orderId) &&
                Enum.TryParse(StatusComboBox.Text, out DeliveryStatus newStatus))
            {
                _deliveryFacade.UpdateOrderStatus(orderId, newStatus);
                StatusResultTextBlock.Text = $"Статус обновлен: {newStatus}";
            }
            else
            {
                MessageBox.Show("Введите корректный ID заказа и выберите статус.");
            }
        }
    }
}