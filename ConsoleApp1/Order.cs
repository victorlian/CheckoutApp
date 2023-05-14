using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutApp
{
    public class Order
    {
        public Order ()
        {
            lineItems = new List<LineItem>();
        }
        private readonly IList<LineItem> lineItems;

        public IList<LineItem> LineItems { get => lineItems; }

        public int Count ()
        {
            return lineItems.Count;
        }

        public void AddLineItem(LineItem l)
        {
            lineItems.Add(l);
        }

        public Money GetTotal()
        {
            return lineItems.Aggregate(new Money(0), (total, next) => total += next.PriceAfterDiscount);
        }

        public IList<LineItem> FindLineItemsWithProduct (Product p)
        {
            return lineItems.Where(l => l.Product.Equals(p)).ToList();
        }
    }
}
