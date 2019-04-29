using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreditCardEngine.Test
{
    [TestClass]
    public class CreditCardProcessorTests
    {
        [TestMethod]
        public void CalculateInterestTest()
        {
            Dictionary<ICreditCard, decimal> testCardToExpectedInterest = new Dictionary<ICreditCard, decimal>()
            {
                { new DiscoverCard(100m), 1m },
                { new DiscoverCard(-350m), -3.5m },
                { new MasterCard(100m), 5m },
                { new VisaCard(100m), 10m }
            };

            foreach (ICreditCard card in testCardToExpectedInterest.Keys)
            {
                CreditCardProcessor processor = new CreditCardProcessor(card);
                decimal calculatedInterest = processor.CalculateInterest();
                decimal expectedInterest = testCardToExpectedInterest[card];

                Assert.IsTrue(calculatedInterest == expectedInterest, string.Format("Unexpected interest returned for card [{0}]. Expected [{1}] but received [{2}].", nameof(card), expectedInterest, calculatedInterest));
            }
        }
    }
}
