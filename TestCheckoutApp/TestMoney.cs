using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckoutApp;
using System.Collections.Generic;

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

        [DataRow(1.99, 33, 1.33)]
        [DataRow(1.01, 50, 0.51)] // Round up
        [DataRow(1.99, 100, 0)]
        [DataRow(1.99, 0, 1.99)]
        public void TestPercentageOff(double original, int percentageOff, double result)
        {
            Money og = new Money(original);
            Money res = og.PercentageOff(percentageOff);

            Assert.AreEqual(result, res.Value);
        }
    }
}
