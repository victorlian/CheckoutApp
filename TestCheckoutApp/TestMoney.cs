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

        [DataRow(1.99, 33, 1.33)]
        [DataRow(1.01, 50, 0.51)] // Round up
        [DataRow(1.99, 100, 0)]
        [DataRow(1.99, 0, 1.99)]
        [DataTestMethod]
        public void TestPercentageOff(double original, int percentageOff, double result)
        {
            Money og = new Money(original);
            Money res = og.PercentageOff(percentageOff);

            Assert.AreEqual(result, res.Value);
        }

        [DataRow(1.99, "$1.99")]
        [DataRow(1, "$1.00")]
        [DataRow(0.5, "$0.50")]
        [DataRow(0.600000000000000001, "$0.60")]
        [DataRow(0, "$0.00")]
        [DataTestMethod]
        public void TestMoneyFormat(double value, string expectedDisplay)
        {
            Money m = new Money(value);
            string actualDisplay = m.ToString();

            Assert.AreEqual(expectedDisplay, actualDisplay);
        }

        [DataRow(1.99, 1.99, true)]
        [DataRow(0, 0, true)]
        [DataRow(0.999999999999, 1, true)]
        [DataRow(17.99000000002, 17.99, true)]
        [DataRow(16.595, 16.59, false)]
        [DataTestMethod]
        public void TestMoneyEquals(double value1, double value2, bool areEqual)
        {
            Money m1 = new Money(value1);
            Money m2 = new Money(value2);

            Assert.IsTrue(m1.Equals(m2) == areEqual);
        }
    }
}
