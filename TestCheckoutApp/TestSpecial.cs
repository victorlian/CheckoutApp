using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CheckoutApp;

namespace TestCheckoutApp
{
    [TestClass]
    public class TestSpecial
    {
        [TestMethod]
        public void TestAmountOffSpecialSingle()
        {
            Product item = new Product("p", new Money(1.99));
            ISpecial s = new AmountOffSpecial(new Money(0.98), item);

            Checkout c = new Checkout();
            c.AddSpecial(s);
            c.ScanItem(item);

            Order o = c.Order;
            Assert.AreEqual(new Money(1.01), o.GetTotal());
        }
    }
}
