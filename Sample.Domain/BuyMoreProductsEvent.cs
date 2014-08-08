using LiteFx.DomainEvents;
using Microsoft.Practices.ServiceLocation;
using Sample.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Domain
{
    public class BuyMoreProductsEvent : IDomainEvent
    {
        public int ProductId { get; set; }
        public double Quantity { get; set; }

        public BuyMoreProductsEvent(int productId, double quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }

    public class BuyMoreProductsEventHandler : IAsyncDomainEventHandler<BuyMoreProductsEvent>
    {

        public void HandleDomainEvent(BuyMoreProductsEvent buyMoreProductsEvent)
        {
            Product product = ServiceLocator.Current.GetInstance<IProductRepository>().GetById(buyMoreProductsEvent.ProductId);
            product.Quantity += buyMoreProductsEvent.Quantity;
            System.Threading.Thread.Sleep(5000);
        }
    }
}
