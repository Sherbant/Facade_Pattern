using System;
using System.Collections.Generic;
using static Facade_Pattern.DeliveryStatusTracker;

namespace Facade_Pattern
{
    public class DeliveryFacade
    {
        private readonly DeliveryStatusTracker _statusTracker;
        private readonly DeliveryCostCalculator _costCalculator;
        private readonly List<Order> _orders; // Список для хранения заказов
        private int _nextOrderId = 1; // Счетчик для уникальных ID

        public DeliveryFacade()
        {
            _statusTracker = new DeliveryStatusTracker();
            _costCalculator = new DeliveryCostCalculator();
            _orders = new List<Order>();
        }

        public int PlaceOrder(string orderDetails)
        {
            int orderId = _nextOrderId++;
            _statusTracker.SetStatus(orderId, DeliveryStatus.OrderPlaced);

            // Добавляем новый заказ в список
            _orders.Add(new Order
            {
                Id = orderId,
                Details = orderDetails,
                Status = DeliveryStatus.OrderPlaced
            });

            return orderId;
        }

        public string GetOrderInfo(int orderId)
        {
            var order = _orders.Find(o => o.Id == orderId);
            return order != null ? $"Заказ ID: {orderId}, Детали: {order.Details}, Статус: {GetLocalizedStatus(order.Status)}" : "Заказ не найден.";
        }

        public double GetDeliveryCost(double distance, double weight)
        {
            return _costCalculator.CalculateCost(distance, weight);
        }

        public DeliveryStatus GetOrderStatus(int orderId)
        {
            var order = _orders.Find(o => o.Id == orderId);
            return order != null ? order.Status : throw new InvalidOperationException("Заказ не найден.");
        }

        public void UpdateOrderStatus(int orderId, DeliveryStatus status)
        {
            var order = _orders.Find(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;
                _statusTracker.SetStatus(orderId, status);
            }
            else
            {
                throw new InvalidOperationException("Заказ не найден.");
            }
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
    }

    public class Order
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public DeliveryStatus Status { get; set; }
    }
}
