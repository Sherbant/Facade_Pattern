using System;
using System.Collections.Generic;
using System.Linq;

namespace Facade_Pattern
{
    public class OrderManager
    {
        private int _nextOrderId = 1;  // Переменная для уникального идентификатора заказа
        private List<Order> _orders;

        public class Order
        {
            public int OrderId { get; set; }
            public string Details { get; set; }
            public double Cost { get; set; }
            public DeliveryStatusTracker.DeliveryStatus Status { get; set; }
        }

        public OrderManager()
        {
            _orders = new List<Order>();
        }

        public void AddOrder(int orderId, string orderDetails, double cost)
        {
            var order = new Order
            {
                OrderId = orderId,
                Details = orderDetails,
                Cost = cost,
                Status = DeliveryStatusTracker.DeliveryStatus.OrderPlaced
            };
            _orders.Add(order);
        }

        public Order GetOrder(int orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public string GetOrderInfo(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                return $"Информация о заказе {orderId}: Детали: {order.Details}, Стоимость: {order.Cost}, Статус: {order.Status}";
            }
            else
            {
                return $"Заказ с ID {orderId} не найден.";
            }
        }

        public List<Order> GetAllOrders()
        {
            return _orders;
        }
    }
}
