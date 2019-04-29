using CreditCardEngine;
using CustomerCreditEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Note: I would typically separate these out as (1) unit test for per wallet, (2) unit test for per card, and (3) unit test per person.
/// But I combined them so that each test method follows the bullet points from the Word doc.
/// </summary>
namespace ClearentTestCases
{
    [TestClass]
    public class TestCases
    {
        [TestMethod]
        public void TestCaseA()
        {
            decimal expectedDiscoverInterest = 1m;
            decimal expectedMasterInterest = 5m;
            decimal expectedVisaInterest = 10m;
            decimal expectedTotalInterest = expectedDiscoverInterest + expectedMasterInterest + expectedVisaInterest;

            Dictionary<ICreditCard, decimal> cardToExpectedInterest = new Dictionary<ICreditCard, decimal>()
            {
                { new DiscoverCard(100m), expectedDiscoverInterest },
                { new MasterCard(100m), expectedMasterInterest },
                { new VisaCard(100m), expectedVisaInterest }
            };

            //Interest per card
            foreach (ICreditCard card in cardToExpectedInterest.Keys)
            {
                CreditCardProcessor cardProcessor = new CreditCardProcessor(card);
                decimal calculatedInterest = cardProcessor.CalculateInterest();
                decimal expectedInterest = cardToExpectedInterest[card];
                Assert.IsTrue(calculatedInterest == expectedInterest, "Unexpected interest calculated for card [{0}]. Expected [{1}] but received [{2}].", nameof(card), expectedInterest, calculatedInterest);
            }

            //Total interest for customer
            List<Wallet> wallets = new List<Wallet>()
            {
                new Wallet(cardToExpectedInterest.Keys.ToList())
            };

            Customer cust = new Customer("A", wallets);
            CustomerCreditCardProcessor customerCreditCardProcessor = new CustomerCreditCardProcessor(cust);
            decimal calculatedTotalInterest = customerCreditCardProcessor.GetCustomerTotalInterest();
            Assert.IsTrue(calculatedTotalInterest == expectedTotalInterest, "Unexpected total interest calculated for customer ID [{0}]. Expected [{1}] but received [{2}].", cust.CustomerID, expectedTotalInterest, calculatedTotalInterest);
        }

        [TestMethod]
        public void TestCaseB()
        {
            decimal walletOneExpectedInterest = 11m;
            decimal walletTwoExpectedInterest = 5m;
            decimal expectedTotalInterest = walletOneExpectedInterest + walletTwoExpectedInterest;

            Dictionary<Wallet, decimal> walletToExpectedInterest = new Dictionary<Wallet, decimal>()
            {
                { new Wallet(new List<ICreditCard>() { new DiscoverCard(100m), new VisaCard(100m) }), walletOneExpectedInterest },
                { new Wallet(new List<ICreditCard>() { new MasterCard(100m) }), walletTwoExpectedInterest }
            };

            Customer cust = new Customer("B", walletToExpectedInterest.Keys.ToList());
            CustomerCreditCardProcessor customerCreditCardProcessor = new CustomerCreditCardProcessor(cust);
            Dictionary<Wallet, decimal> walletToCalculatedInterest = customerCreditCardProcessor.GetInterestPerWallet();

            foreach (Wallet wallet in walletToCalculatedInterest.Keys)
            {
                decimal calculatedInterest = walletToCalculatedInterest[wallet];
                decimal expectedInterest = walletToExpectedInterest[wallet];
                Assert.IsTrue(calculatedInterest == expectedInterest, "Unexpected interest calculated for wallet [{0}]. Expected [{1}] but received [{2}].", nameof(Wallet), expectedInterest, calculatedInterest);
            }

            decimal calculatedTotalInterest = customerCreditCardProcessor.GetCustomerTotalInterest();
            Assert.IsTrue(calculatedTotalInterest == expectedTotalInterest, "Unexpected total interest calculated for customer ID [{0}]. Expected [{1}] but received [{2}].", cust.CustomerID, expectedTotalInterest, calculatedTotalInterest);
        }

        [TestMethod]
        public void TestCaseC()
        {
            //Person 1 calculations. Total interest per wallet and per person will be the same since each person only has 1 wallet.
            decimal personOneExpectedInterest = 20m;
            Dictionary<Wallet, decimal> walletOneToExpectedInterest = new Dictionary<Wallet, decimal>()
            {
                { new Wallet(new List<ICreditCard>() { new MasterCard(100m), new MasterCard(100m), new VisaCard(100m)}), personOneExpectedInterest}
            };

            Customer custOne = new Customer("C", walletOneToExpectedInterest.Keys.ToList());
            CustomerCreditCardProcessor customerOneCreditCardProcessor = new CustomerCreditCardProcessor(custOne);

            decimal calculatedTotalInterestPersonOne = customerOneCreditCardProcessor.GetCustomerTotalInterest();
            Assert.IsTrue(personOneExpectedInterest == calculatedTotalInterestPersonOne, "Unexpected total interest calculated for customer ID [{0}]. Expected [{1}] but received [{2}].", custOne.CustomerID, personOneExpectedInterest, calculatedTotalInterestPersonOne);

            Dictionary<Wallet, decimal> walletOneToCalculatedInterest = customerOneCreditCardProcessor.GetInterestPerWallet();
            foreach (Wallet wallet in walletOneToCalculatedInterest.Keys)
            {
                decimal calculatedInterest = walletOneToCalculatedInterest[wallet];
                decimal expectedInterest = walletOneToExpectedInterest[wallet];
                Assert.IsTrue(calculatedInterest == expectedInterest, "Unexpected interest calculated for wallet [{0}]. Expected [{1}] but received [{2}].", nameof(Wallet), expectedInterest, calculatedInterest);
            }

            //Person 2 calculations
            decimal personTwoExpectedInterest = 15m;
            Dictionary<Wallet, decimal> walletTwoToExpectedInterest = new Dictionary<Wallet, decimal>()
            {
                { new Wallet(new List<ICreditCard>() { new MasterCard(100m), new VisaCard(100m)}), personTwoExpectedInterest}
            };

            Customer custTwo = new Customer("C", walletTwoToExpectedInterest.Keys.ToList());
            CustomerCreditCardProcessor customerTwoCreditCardProcessor = new CustomerCreditCardProcessor(custTwo);

            decimal calculatedTotalInterestPersonTwo = customerTwoCreditCardProcessor.GetCustomerTotalInterest();
            Assert.IsTrue(personTwoExpectedInterest == calculatedTotalInterestPersonTwo, "Unexpected total interest calculated for customer ID [{0}]. Expected [{1}] but received [{2}].", custTwo.CustomerID, personOneExpectedInterest, calculatedTotalInterestPersonOne);

            Dictionary<Wallet, decimal> walletTwoToCalculatedInterest = customerTwoCreditCardProcessor.GetInterestPerWallet();
            foreach (Wallet wallet in walletTwoToCalculatedInterest.Keys)
            {
                decimal calculatedInterest = walletTwoToCalculatedInterest[wallet];
                decimal expectedInterest = walletTwoToExpectedInterest[wallet];
                Assert.IsTrue(calculatedInterest == expectedInterest, "Unexpected interest calculated for wallet [{0}]. Expected [{1}] but received [{2}].", nameof(Wallet), expectedInterest, calculatedInterest);
            }
        }
    }
}
