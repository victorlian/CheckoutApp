using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class Checkout
    {
        public Order Order { get; set; }

        public IList<ISpecial> Specials { get; set; }

        public Checkout()
        {
            Specials = new List<ISpecial>();
        }

        public void ScanItem(Product p)
        {
            if (Order == null)
            {
                Order = new Order();
            }

            LineItem l = new LineItem(p);
            foreach (ISpecial special in Specials)
            {
                special.ApplySpecial(Order, l);
            }
            Order.AddLineItem(l);
        }

        public void AddSpecial(ISpecial special)
        {
            Specials.Add(special);
        }

        public string Display()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0, -40}{1, 9}", "SKU", "Price"));
            sb.AppendLine("=======================================================");

            foreach (LineItem lineItem in Order.LineItems)
            {
                sb.AppendLine(string.Format("{0, -40}{1, 9}", lineItem.Product.SKU, " " + lineItem.PriceBeforeDiscount));

                if (lineItem.Special != null)
                {
                    sb.AppendLine(string.Format("{0, -40}{1, 9}", ">>>" + lineItem.Special.Name, "-" + lineItem.Discount));
                }
            }

            sb.AppendLine("=======================================================");
            sb.AppendLine(string.Format("{0, -40}{1, 9}", " Total", " " + Order.GetTotal()));
            return sb.ToString();
        }
    }
}
