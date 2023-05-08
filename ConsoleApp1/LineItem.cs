using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class LineItem
    {
        public LineItem (Product p)
        {
            Product = p;
            PriceBeforeDiscount = p.Price;
        }

        public Product Product { get; set; }

        public Money PriceBeforeDiscount { get; set; }
    }
}
