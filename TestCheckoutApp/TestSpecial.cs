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
        const string SPECIAL_1_NAME = "99 cents off P1";

        Product product1;
        ISpecial amountOffSpecial1;

        public TestSpecial()
        {
            product1 = new Product("p", new Money(PRODUCT_1_PRICE));
            amountOffSpecial1 = new AmountOffSpecial(SPECIAL_1_NAME, new Money(PRODUCT_1_DISCOUNT), product1);
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

            // Make sure special is recorded in lineitem
            IList<LineItem> itemsWithP1 = o.FindLineItemsWithProduct(product1);
            Assert.AreEqual(2, itemsWithP1.Count);
            foreach (LineItem item in itemsWithP1) {                
                Assert.AreEqual(amountOffSpecial1, item.Special);
                Assert.AreEqual(SPECIAL_1_NAME, item.Special.Name);
                Assert.AreEqual(new Money(PRODUCT_1_DISCOUNT), item.Discount);
            }
        }

        [TestMethod]
        public void TestAmountOffSpecialMultipleItems()
        {
            Product product2 = new Product("p2", new Money(2.00));
            string s2Name = "50 cents off p2";
            double product2Discount = 0.5;
            ISpecial s2 = new AmountOffSpecial(s2Name, new Money(product2Discount), product2);

            Checkout c = new Checkout();
            c.AddSpecial(amountOffSpecial1);
            c.AddSpecial(s2);

            c.ScanItem(product1);
            c.ScanItem(product2);
            c.ScanItem(product1);

            Order o = c.Order;
            Assert.AreEqual(new Money(PRODUCT_1_NEW_PRICE * 2 + 1.5), o.GetTotal());

            // Make sure special is recorded in lineitem
            foreach (LineItem item in o.LineItems)
            {
                if (item.Product.Equals(product1))
                {
                    Assert.AreEqual(amountOffSpecial1, item.Special);
                    Assert.AreEqual(new Money(PRODUCT_1_DISCOUNT), item.Discount);
                }
                else
                {
                    Assert.AreEqual(s2, item.Special);
                    Assert.AreEqual(s2Name, item.Special.Name);
                    Assert.AreEqual(new Money(product2Discount), item.Discount);
                }
            }
        }

        [TestMethod]
        public void TestPercentageOffSpecial()
        {
            int percentageOff = 33;
            double price = 3.99;
            double newPrice = Math.Round(3.99 * (100 - percentageOff) / 100, 2);
            double discount = price - newPrice;

            Product percentageOffProduct = new Product("p3", new Money(3.99));

            ISpecial s3 = new PercentageOffSpecial("33% off p3", percentageOff, percentageOffProduct);

            Checkout c = new Checkout();
            c.AddSpecial(amountOffSpecial1);
            c.AddSpecial(s3);

            c.ScanItem(percentageOffProduct);

            Order o = c.Order;
            Assert.AreEqual(new Money(newPrice), o.GetTotal());
            Assert.AreEqual(new Money(discount), o.LineItems[0].Discount);
        }

        [TestMethod]
        public void TestMultiBuySpecial()
        {
            double amountOff = 2.00;
            double price = 6.00;
            Product discountProduct = new Product("p4", new Money(price));

            ISpecial s4 = new MultiBuySpecial("Buy two p4 for 10", 2, new Money(amountOff), discountProduct);

            Checkout c = new Checkout();
            c.AddSpecial(s4);

            // first buy
            c.ScanItem(discountProduct);
            Order o = c.Order;
            Assert.AreEqual(new Money(price), o.GetTotal());
            Assert.AreEqual(new Money(price), o.LineItems[0].PriceBeforeDiscount);
            Assert.IsNull(o.LineItems[0].Special);

            // buy something unrelated
            c.ScanItem(product1);
            Assert.AreEqual(new Money(price + PRODUCT_1_PRICE), o.GetTotal());
            Assert.AreEqual(new Money(PRODUCT_1_PRICE), o.LineItems[1].PriceBeforeDiscount);

            // second buy, apply discount
            c.ScanItem(discountProduct);
            Assert.AreEqual(new Money(price * 2 + PRODUCT_1_PRICE - amountOff), o.GetTotal());
            Assert.AreEqual(new Money(price), o.LineItems[2].PriceBeforeDiscount);
            Assert.AreEqual(new Money(amountOff), o.LineItems[2].Discount);
            Assert.AreEqual(new Money(price - amountOff), o.LineItems[2].PriceAfterDiscount);
            Assert.AreEqual(s4, o.LineItems[0].Special);
            Assert.AreEqual(s4, o.LineItems[2].Special);

            // third buy, no discount
            c.ScanItem(discountProduct);
            Assert.AreEqual(new Money(price * 3 + PRODUCT_1_PRICE - amountOff), o.GetTotal());
            Assert.AreEqual(new Money(price), o.LineItems[3].PriceBeforeDiscount);
            Assert.AreEqual(new Money(0), o.LineItems[3].Discount);
            Assert.IsNull(o.LineItems[3].Special);

            // forth buy, apply discount (twice)
            c.ScanItem(discountProduct);
            Assert.AreEqual(new Money(price * 4 + PRODUCT_1_PRICE - amountOff * 2), o.GetTotal());
            Assert.AreEqual(new Money(price), o.LineItems[4].PriceBeforeDiscount);
            Assert.AreEqual(new Money(amountOff), o.LineItems[4].Discount);
            Assert.AreEqual(new Money(price - amountOff), o.LineItems[4].PriceAfterDiscount);
            Assert.AreEqual(s4, o.LineItems[3].Special);
            Assert.AreEqual(s4, o.LineItems[4].Special);
        }

        //public void TestMultipleSpecials()
        //{
        //    double amountOff = 2.00;
        //    double price = 6.00;
        //    Product discountProduct = new Product("p4", new Money(9.99));
        //    ISpecial s4 = new MultiBuySpecial("Buy two p4 for 10", 2, new Money(amountOff), discountProduct);

        //    double amountOff2 = 3.00;
        //    double price2 = 7.00;
        //    Product discountProduct2 = new Product("p5", new Money())

        //    Checkout c = new Checkout();
        //    c.AddSpecial(s4);
        //}
    }
}
