using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutApp
{
    public class MultiBuySpecial : ISpecial
    {
        public MultiBuySpecial(string name, int quantityNeeded, Money amountOff, Product product)
        {
            Name = name;
            QuantityNeeded = quantityNeeded;
            AmountOff = amountOff;
            Product = product;
        }

        public string Name { get; }

        public int QuantityNeeded { get; }

        public Product Product { get; set; }

        public Money AmountOff { get; }

        public void ApplySpecial(Order order, LineItem lineItem)
        {
            IList<LineItem> matchingLineItems = order.LineItems
                                                    .Where(l => l.Product.Equals(Product) && l.Special == null)
                                                    .ToList();
            if (matchingLineItems.Count >= QuantityNeeded)
            {
                matchingLineItems.Take(QuantityNeeded)
                    .ToList()
                    .ForEach(l => l.Special = this);
            }
            lineItem.Discount = AmountOff;
        }
    }
}
