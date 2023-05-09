using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class Checkout
    {
        public Order Order { get; set; }

        public AmountOffSpecial Special { get; set; }

        public void ScanItem (Product p)
        {
            if (Order == null)
            {
                Order = new Order();
            }

            Order.AddLineItem(new LineItem(p), Special);
        }

        public void SetSpecial (AmountOffSpecial special)
        {
            Special = special;
        }

        public string Display ()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0, -12}{1, 6}", "SKU", "Price"));
            sb.AppendLine("==============================");

            foreach (LineItem lineItem in Order.LineItems) {
                sb.AppendLine(string.Format("{0, -12}{1, 6}", lineItem.Product.SKU, lineItem.PriceBeforeDiscount));
            }

            sb.AppendLine("==============================");
            sb.AppendLine(string.Format("{0, -12}{1, 6}", "Total", Order.GetTotal()));
            return sb.ToString();
        }
    }
}
