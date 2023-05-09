using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class AmountOffSpecial
    {
        public AmountOffSpecial (Money amountOff)
        {
            AmountOff = amountOff;
        }
        public Money AmountOff { get; }

        public void ApplySpecial (LineItem l)
        {
            l.Discount += AmountOff;
            l.PriceAfterDiscount -= AmountOff;
        }
    }
}
