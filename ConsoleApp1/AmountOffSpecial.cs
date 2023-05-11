using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class AmountOffSpecial : ISpecial
    {
        public AmountOffSpecial (Money amountOff, Product product)
        {
            AmountOff = amountOff;
            Product = product;
        }
        public Money AmountOff { get; }

        public Product Product { get; set; }

        public void ApplySpecial (LineItem l)
        {
            if (l.Product == Product)
            {
                l.Discount += AmountOff;
                l.PriceAfterDiscount -= AmountOff;
                l.AddSpecial(this);
            }
        }
    }
}
