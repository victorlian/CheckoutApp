using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutApp;

namespace TestCheckoutApp
{
    [TestClass]
    public class TestCheckOut
    {
        [TestMethod]
        public void TestCanCreateOrderUponFirstScan()
        {
            Checkout c = new Checkout();
            
            Product item = new Product('p', 1.99);
            c.ScanItem(item);

            Assert.IsNotNull(c.Order);
            Assert.AreEqual(1, c.Order.Count());
        }
    }
}
