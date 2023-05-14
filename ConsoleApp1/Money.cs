using System;

namespace CheckoutApp
{
    public class Money
    {
        public Money (double value)
        {
            Value = value;
        }

        public Money ()
        {
            Value = 0;
        }

        public Money (Money other)
        {
            Value = other.Value;
        }

        public double Value { get; set; }

        public static Money operator +(Money a, Money b) => new Money(a.Value + b.Value);

        public static Money operator -(Money a, Money b) => new Money(a.Value - b.Value);

        public Money PercentageOff(int percentageOff)
        {
            if (percentageOff > 100 || percentageOff < 0)
            {
                throw new ArgumentOutOfRangeException("Expect percentage off between 0 and 100");
            }

            return new Money(Math.Round(Value * (100 - percentageOff) / 100, 2, MidpointRounding.AwayFromZero)); // Round up if value is 0.005
        }

        public override bool Equals(object obj)
        {
            double acceptedVariance = 1E-9;
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Money other = (Money)obj;
            return Math.Abs(this.Value - other.Value) < acceptedVariance;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return '$' + Value.ToString("0.00");
        }
    }
}
