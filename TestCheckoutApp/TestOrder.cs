﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            LineItem lineItem = new LineItem(p1);
            LineItem lineItem = new LineItem(p2);

            Assert.Equals(2, o.Count);
        }
    }
}
