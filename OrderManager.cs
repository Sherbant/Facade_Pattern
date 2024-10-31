using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_Pattern
{
    public class OrderManager
    {
        public void AddOrder(string orderDetails)
        {
        }

        public string GetOrderInfo(int orderId)
        {
            return $"Order info for {orderId}";
        }
    }
}
