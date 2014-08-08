using LiteFx.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Domain
{
    public class SellService
    {
        public static void Sell(int productId, double quantity) 
        {
            DomainEvents.Raise(new ProductSoldEvent(productId, quantity));
        }
    }
}