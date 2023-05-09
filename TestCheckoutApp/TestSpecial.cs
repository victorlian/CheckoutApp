using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CheckoutApp;

namespace TestCheckoutApp
{
    [TestClass]
    public class TestSpecial
    {
        [TestMethod]
        public void TestAmountOffSpecial()
        {
            AmountOffSpecial s = new AmountOffSpecial(new Money(0.98);

            Product item = new Product("p", new Money(1.99));

            Checkout c = new Checkout();
            c.setSpecial(s)
            c.ScanItem(item);

            Order o = c.Order;
            Assert.AreEqual(new Money(1.01), o.GetTotal());
        }
    }
}
