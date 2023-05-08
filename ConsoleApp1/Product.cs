using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    class Product
    {
        public Product (string sku, Money price)
        {
            SKU = sku;
            Price = price;
        }

        public string SKU { get; set; }

        public Money Price { get; }
    }
}
