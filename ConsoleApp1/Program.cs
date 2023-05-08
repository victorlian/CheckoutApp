using System;

namespace CheckoutApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Product p1 = new Product("p1", new Money(1.99));
            Product p2 = new Product("p2", new Money(2.99));
            Product p3 = new Product("p3", new Money(3.99));

            Checkout c = new Checkout();

            c.ScanItem(p1);
            c.ScanItem(p2);
            c.ScanItem(p3);

            Console.Write(c.Display());
        }
    }
}
