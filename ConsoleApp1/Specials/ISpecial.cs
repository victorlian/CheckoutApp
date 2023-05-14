using System;

namespace CheckoutApp
{
    public interface ISpecial
    {
        public string Name { get; }

        public void ApplySpecial(Order order, LineItem lineItem);
    }
}
