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
            PriceBeforeDiscount = new Money(p.Price);
            Discount = new Money();
            PriceAfterDiscount = new Money(PriceBeforeDiscount);
        }

        public Product Product { get; set; }

        public Money PriceBeforeDiscount { get; set; }

        public Money Discount { get; set; }

        public Money PriceAfterDiscount { get; set; }
    }
}
