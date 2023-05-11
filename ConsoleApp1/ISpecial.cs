using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public interface ISpecial
    {
        public string Name { get; }

        public void ApplySpecial(Order order, LineItem lineItem);
    }
}
