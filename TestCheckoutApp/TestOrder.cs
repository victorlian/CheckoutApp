using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutApp;

namespace TestCheckoutApp
{
    [TestClass]
    public class TestOrder
    {
        private readonly Product p1;
        private readonly Product p2;

        public TestOrder()
        {
            p1 = new Product("P1", new Money(1.99));
            p2 = new Product("P2", new Money(2.99));
        }

        [TestMethod]
        public void TestOrderAddLineItems()
        {
            Order o = new Order();
            LineItem l1 = new LineItem(p1);
            LineItem l2 = new LineItem(p2);
            o.AddLineItem(l1);
            o.AddLineItem(l2);

            Assert.AreEqual(2, o.Count());
        }

        public void TestOrderTotalValueSingleLineItem()
        {
            Order o = new Order();
            LineItem l1 = new LineItem(p1);
            o.AddLineItem(l1);

            Assert.AreEqual(new Money(1.99), o.GetTotal());
        }

        public void TestOrderTotalValueMultipleLineItems()
        {
            Order o = new Order();
            LineItem l1 = new LineItem(p1);
            LineItem l2 = new LineItem(p2);
            o.AddLineItem(l1);
            o.AddLineItem(l2);

            Assert.AreEqual(4.98, o.GetTotal());
        }
    }
}
