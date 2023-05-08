using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class Checkout
    {
        public Order Order { get; set; }

        public void ScanItem (Product p)
        {
            if (Order == null)
            {
                Order = new Order();
            }

            Order.AddLineItem(new LineItem(p));
        }
    }
}
