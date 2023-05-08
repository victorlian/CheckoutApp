using System;
using System.Collections.Generic;
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
    }
}
