using System;
using System.Windows;
using static Facade_Pattern.DeliveryStatusTracker;

namespace Facade_Pattern
{
    public partial class MainWindow : Window
    {
        private readonly DeliveryFacade _deliveryFacade;

        public MainWindow()
        {
            InitializeComponent();
            _deliveryFacade = new DeliveryFacade();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            string orderDetails = OrderDetailsTextBox.Text;
            if (double.TryParse(DistanceTextBox.Text, out double distance) &&
                double.TryParse(WeightTextBox.Text, out double weight))
            {
                // Используем PlaceOrder и сохраняем ID заказа
                int orderId = _deliveryFacade.PlaceOrder(orderDetails);
                double deliveryCost = _deliveryFacade.GetDeliveryCost(distance, weight);

                string orderInfo = $"ID: {orderId}, Детали: {orderDetails}, " +
                                   $"Расстояние: {distance} км, Вес: {weight} кг, Стоимость: {deliveryCost} руб.";
                OrderListBox.Items.Add(orderInfo);

                // Очищаем поля ввода
                OrderDetailsTextBox.Clear();
                DistanceTextBox.Clear();
                WeightTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное расстояние и вес.");
            }
        }

        private void CalculateCostButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(DistanceTextBox.Text, out double distance) &&
                double.TryParse(WeightTextBox.Text, out double weight))
            {
                double cost = _deliveryFacade.GetDeliveryCost(distance,weight);
                CostResultTextBlock.Text = $"Стоимость доставки: {cost:C}";
            }
            else
            {
                MessageBox.Show("Введите правильные значения для расстояния и веса.");
            }
        }

        private void CheckOrderInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(OrderIdTextBox.Text, out int orderId))
            {
                string orderInfo = _deliveryFacade.GetOrderInfo(orderId);
                StatusResultTextBlock.Text = orderInfo;
            }
            else
            {
                MessageBox.Show("Введите корректный ID заказа.");
            }
        }

        private void CheckStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(OrderIdTextBox.Text, out int orderId))
            {
                DeliveryStatus status = _deliveryFacade.GetOrderStatus(orderId);
                StatusResultTextBlock.Text = $"Статус заказа: {_deliveryFacade.GetLocalizedStatus(status)}";
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
                StatusResultTextBlock.Text = $"Статус обновлен: {_deliveryFacade.GetLocalizedStatus(newStatus)}";
            }
            else
            {
                MessageBox.Show("Введите корректный ID заказа и выберите статус.");
            }
        }
    }
}
