using LiteFx;
using LiteFx.DomainEvents;
using Microsoft.Practices.ServiceLocation;
using Sample.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Domain
{
    public class ProductSoldEvent : IDomainEvent
    {
        public int ProductId { get; set; }
        public double Quantity { get; set; }

        public ProductSoldEvent(int productId, double quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }

    public class ProductSoldEventHandler : IAsyncDomainEventHandler<ProductSoldEvent>
    {
        public void HandleDomainEvent(ProductSoldEvent productSoldEvent)
        {
            Product product = ServiceLocator.Current.GetInstance<IProductRepository>().GetById(productSoldEvent.ProductId);

            if (productSoldEvent.Quantity > product.Quantity)
                //throw new BusinessException("Insuficient Quantity.");
                DomainEvents.Raise(new BuyMoreProductsEvent(product.Id, productSoldEvent.Quantity - product.Quantity));
            
            product.Quantity -= productSoldEvent.Quantity;

            System.Threading.Thread.Sleep(5000);            
        }
    }
}
