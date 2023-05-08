using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckoutApp
{
    public class Order
    {
        public Order ()
        {
            lineItems = new List<LineItem>();
        }
        public IList<LineItem> lineItems;

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
            return lineItems.Aggregate(new Money(0), (total, next) => total += next.PriceBeforeDiscount);
        }
    }
}
