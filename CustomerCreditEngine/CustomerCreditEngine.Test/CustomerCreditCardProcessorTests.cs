using CustomerCreditEngine;
using System.Collections.Generic;
using CreditCardEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerCreditEngine.Test
{
    [TestClass]
    public class CustomerCreditCardProcessorTests
    {
        [TestMethod]
        public void GetCustomerTotalInterestTest_OneWallet()
        {
            decimal expectedTotalInterestForCustomer = 16m;
            List<Wallet> customerWallets = new List<Wallet>()
            {
                { new Wallet(new List<ICreditCard>() { new DiscoverCard(100m), new MasterCard(100m), new VisaCard(100m) }) }
            };

            Customer testCustomer = new Customer("111", customerWallets);
            CustomerCreditCardProcessor customerProcessor = new CustomerCreditCardProcessor(testCustomer);
            decimal calculatedTotalInterestForCustomer = customerProcessor.GetCustomerTotalInterest();

            Assert.IsTrue(expectedTotalInterestForCustomer == calculatedTotalInterestForCustomer,
                string.Format("Unexpected total interest encountered for test customer [{0}] with one wallet. Expected [{1}] but received [{2}].", testCustomer.CustomerID, expectedTotalInterestForCustomer, calculatedTotalInterestForCustomer));
        }

        [TestMethod]
        public void GetCustomerTotalInterestTest_MultipleWallets()
        {
            decimal expectedTotalInterestForCustomer = 16m;
            List<Wallet> customerWallets = new List<Wallet>()
            {
                { new Wallet(new List<ICreditCard>() { new DiscoverCard(100m), new VisaCard(100m) }) },
                { new Wallet(new List<ICreditCard>() { new MasterCard(100m) }) }
            };

            Customer testCustomer = new Customer("222", customerWallets);
            CustomerCreditCardProcessor customerProcessor = new CustomerCreditCardProcessor(testCustomer);
            decimal calculatedTotalInterestForCustomer = customerProcessor.GetCustomerTotalInterest();

            Assert.IsTrue(expectedTotalInterestForCustomer == calculatedTotalInterestForCustomer,
                string.Format("Unexpected total interest encountered for test customer [{0}] with multiple wallets. Expected [{1}] but received [{2}].", testCustomer.CustomerID, expectedTotalInterestForCustomer, calculatedTotalInterestForCustomer));
        }

        [TestMethod]
        public void GetCustomerInterestPerWalletTest()
        {
            List<Wallet> customerWallets = new List<Wallet>()
            {
                { new Wallet(new List<ICreditCard>() { new DiscoverCard(100m), new VisaCard(100m) }) },
                { new Wallet(new List<ICreditCard>() { new MasterCard(100m) }) }
            };

            Dictionary<Wallet, decimal> walletToExpectedInterest = new Dictionary<Wallet, decimal>()
            {
                { customerWallets[0], 11m },
                { customerWallets[1], 5m }
            };

            Customer testCustomer = new Customer("333", customerWallets);
            CustomerCreditCardProcessor customerProcessor = new CustomerCreditCardProcessor(testCustomer);
            Dictionary<Wallet, decimal> walletToCalculatedInterest = customerProcessor.GetInterestPerWallet();

            foreach (Wallet calculatedWallet in walletToCalculatedInterest.Keys)
            {
                decimal expectedInterest = walletToExpectedInterest[calculatedWallet];
                decimal calculatedInterest = walletToCalculatedInterest[calculatedWallet];

                Assert.IsTrue(expectedInterest == calculatedInterest);
            }
        }
    }
}
