using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_Pattern
{
    public class DeliveryStatusTracker
    {
        public enum DeliveryStatus
        {
            OrderPlaced,    // Заказ принят
            Processing,     // В обработке
            InTransit,      // В пути
            Delivered,      // Доставлен
            Canceled        // Отменён
        }

        private Dictionary<int, DeliveryStatus> _orderStatuses;
        public DeliveryStatusTracker()
        {
            _orderStatuses = new Dictionary<int, DeliveryStatus>();
        }

        public void SetStatus(int orderId, DeliveryStatus status)
        {
            _orderStatuses[orderId] = status;
        }

        public DeliveryStatus GetStatus(int orderId)
        {
            if (_orderStatuses.TryGetValue(orderId, out DeliveryStatus status))
            {
                return status;
            }
            return DeliveryStatus.OrderPlaced; // статус по умолчанию
        }
    }
}

