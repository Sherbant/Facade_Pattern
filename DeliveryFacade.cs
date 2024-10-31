using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Facade_Pattern.DeliveryStatusTracker;

namespace Facade_Pattern
{
    public class DeliveryFacade
    {
        private readonly OrderManager _orderManager;
        private readonly DeliveryCostCalculator _costCalculator;
        private readonly DeliveryStatusTracker _statusTracker;

        public DeliveryFacade()
        {
            _orderManager = new OrderManager();
            _costCalculator = new DeliveryCostCalculator();
            _statusTracker = new DeliveryStatusTracker();
        }

        public string GetLocalizedStatus(DeliveryStatus status)
        {
            return status switch
            {
                DeliveryStatus.OrderPlaced => "Заказ принят",
                DeliveryStatus.Processing => "В обработке",
                DeliveryStatus.InTransit => "В пути",
                DeliveryStatus.Delivered => "Доставлен",
                DeliveryStatus.Canceled => "Отменён",
                _ => "Неизвестный статус"
            };
        }


        public void PlaceOrder(string orderDetails)
        {
            int orderId = GenerateOrderId();  // Генерация уникального ID заказа
            _orderManager.AddOrder(orderDetails);
            _statusTracker.SetStatus(orderId, DeliveryStatus.OrderPlaced);
        }

        public void UpdateOrderStatus(int orderId, DeliveryStatus status)
        {
            _statusTracker.SetStatus(orderId, status);
        }

        public double GetDeliveryCost(double distance, double weight)
        {
            return _costCalculator.CalculateCost(distance, weight);
        }

        public DeliveryStatus GetOrderStatus(int orderId)
        {
            return _statusTracker.GetStatus(orderId);
        }

        private int GenerateOrderId()
        {
            return new Random().Next(1000, 9999); // пример
        }
    }
}
