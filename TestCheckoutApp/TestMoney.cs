using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutApp;

namespace TestCheckoutApp
{
    [TestClass]
    public class TestMoney
    {
        [TestMethod]
        public void TestAddition()
        {
            Money m1 = new Money(1.23);
            Money m2 = new Money(2.35);
            Money m3 = new Money(3.58);
            Assert.AreEqual(m3, m1 + m2);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            Money m1 = new Money(1.99);
            Money m2 = new Money(0.99);
            Money m3 = new Money(1);
            Assert.AreEqual(m3, m1 - m2);
        }
    }
}
