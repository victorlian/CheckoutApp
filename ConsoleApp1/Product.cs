using System;

namespace CheckoutApp
{
    public class Product
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
