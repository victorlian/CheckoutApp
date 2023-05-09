using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public interface ISpecial
    {
        public void ApplySpecial(LineItem l);
    }
}
