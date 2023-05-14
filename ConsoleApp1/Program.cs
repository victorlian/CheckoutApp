using System;

namespace CheckoutApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Product p1 = new Product("p1", new Money(1.99));
            Product p2 = new Product("p2", new Money(2.99));
            Product p3 = new Product("p3", new Money(119.99));

            ISpecial s1 = new AmountOffSpecial("50 cents off p1", new Money(0.5), p1);
            ISpecial s2 = new PercentageOffSpecial("20% off p2", 20, p2);
            ISpecial s3 = new MultiBuySpecial("Buy two p3 for 200", 2, new Money(39.98), p3);

            Checkout c = new Checkout();
            c.AddSpecial(s1);
            c.AddSpecial(s2);
            c.AddSpecial(s3);

            c.ScanItem(p3);
            c.ScanItem(p1);
            c.ScanItem(p2);
            c.ScanItem(p3);

            Console.Write(c.Display());
        }
    }
}
