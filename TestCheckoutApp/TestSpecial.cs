using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Linq;
using CheckoutApp;

namespace TestCheckoutApp
{
    [TestClass]
    public class TestSpecial
    {
        const double PRODUCT_1_PRICE = 1.99;
        const double PRODUCT_1_DISCOUNT = 0.99;
        const double PRODUCT_1_NEW_PRICE = 1;

        Product product1;
        ISpecial amountOffSpecial1;

        public TestSpecial()
        {
            product1 = new Product("p", new Money(PRODUCT_1_PRICE));
            amountOffSpecial1 = new AmountOffSpecial(new Money(PRODUCT_1_DISCOUNT), product1);
        }

        [TestMethod]
        public void TestAmountOffSpecialSingleItem()
        {
            Product product2 = new Product("p2", new Money(2.00));

            Checkout c = new Checkout();
            c.AddSpecial(amountOffSpecial1);

            c.ScanItem(product1);
            c.ScanItem(product2);
            c.ScanItem(product1);

            Order o = c.Order;
            Assert.AreEqual(new Money(PRODUCT_1_NEW_PRICE * 2 + 2), o.GetTotal());

            // Make sure specials are recorded
            IEnumerable<LineItem> itemsWithProduct1 = o.LineItems.Where(l => l.Product.Equals(product1));
            foreach (LineItem item in itemsWithProduct1) {
                IList<ISpecial> specials = item.Specials;
                Assert.IsTrue(specials.Contains(amountOffSpecial1));
            }
        }

        [TestMethod]
        public void TestAmountOffSpecialMultipleItems()
        {
            Product product2 = new Product("p2", new Money(2.00));
            ISpecial s2 = new AmountOffSpecial(new Money(0.5), product2);

            Checkout c = new Checkout();
            c.AddSpecial(amountOffSpecial1);
            c.AddSpecial(s2);

            c.ScanItem(product1);
            c.ScanItem(product2);
            c.ScanItem(product1);

            Order o = c.Order;
            Assert.AreEqual(new Money(PRODUCT_1_NEW_PRICE * 2 + 1.5), o.GetTotal());

            // Make sure specials are recorded
            foreach (LineItem item in o.LineItems)
            {
                IList<ISpecial> specials = item.Specials;

                if (item.Product.Equals(product1))
                {
                    Assert.IsTrue(specials.Contains(amountOffSpecial1));
                }
                else
                {
                    Assert.IsTrue(specials.Contains(s2));
                }
            }
        }

        [TestMethod]
        public void TestPercentageOffSpecial()
        {
            Product percentageOffProduct = new Product("p3", new Money(3.99));
            int percentageOff = 33;
            double newPrice = Math.Round(3.99 * (100 - percentageOff) / 100, 2);
            ISpecial s3 = new PercentageOffSpecial(percentageOff, percentageOffProduct);

            Checkout c = new Checkout();
            c.AddSpecial(amountOffSpecial1);
            c.AddSpecial(s3);

            c.ScanItem(percentageOffProduct);

            Order o = c.Order;
            Assert.AreEqual(new Money(newPrice), o.GetTotal());
        }
    }
}
