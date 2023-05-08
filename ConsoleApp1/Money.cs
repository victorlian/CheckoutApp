using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutApp
{
    public class Money
    {
        public Money (double value)
        {
            Value = value;
        }

        public double Value { get; set; }

        public static Money operator +(Money a, Money b) => new Money(a.Value + b.Value);

        public static Money operator -(Money a, Money b) => new Money(a.Value - b.Value);

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Money other = (Money)obj;
            return this.Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return '$' + Value.ToString();
        }
    }
}
