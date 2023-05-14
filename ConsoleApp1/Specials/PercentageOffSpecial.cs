using System;

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

        public void ApplySpecial(Order order, LineItem lineItem)
        {
            if (lineItem.Product == Product)
            {
                Money resultPrice = lineItem.Product.Price.PercentageOff(PercentageOff);
                Money priceSaved = lineItem.Product.Price - resultPrice;
                lineItem.Discount += priceSaved;
                lineItem.PriceAfterDiscount -= priceSaved;
                lineItem.Special = this;
            }
        }
    }
}
