using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class LineItem
    {
        public Product Product { get; set; }

        public Money Discount { get; set; }
    }
}
