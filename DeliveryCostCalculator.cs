using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade_Pattern
{
    public class DeliveryCostCalculator
    {
        public double CalculateCost(double distance, double weight)
        {
            return distance * weight * 0.5; 
        }
    }
}
