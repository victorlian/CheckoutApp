using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class PercentageOffSpecial : ISpecial
    {
        public PercentageOffSpecial (string name, int percentageOff, Product product)
        {
            Name = name;
            PercentageOff = percentageOff;
            Product = product;
        }

        public string Name { get; }

        public int PercentageOff { get; }

        public Product Product { get; set; }

        public void ApplySpecial (LineItem l)
        {
            if (l.Product == Product)
            {
                Money resultPrice = l.Product.Price.PercentageOff(PercentageOff);
                Money priceSaved = l.Product.Price - resultPrice;
                l.Discount += priceSaved;
                l.PriceAfterDiscount -= priceSaved;
                l.AddSpecial(this);
            }
        }
    }
}
