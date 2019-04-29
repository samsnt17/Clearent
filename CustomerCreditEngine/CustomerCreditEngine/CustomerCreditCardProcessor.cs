using CreditCardEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCreditEngine
{
    public class CustomerCreditCardProcessor
    {
        Customer _customer;

        public CustomerCreditCardProcessor(Customer customer)
        {
            _customer = customer;
        }

        public decimal GetCustomerTotalInterest()
        {
            decimal totalInterest = 0m;

            foreach (Wallet customerWallet in _customer.Wallets)
            {
                foreach (ICreditCard creditCard in customerWallet.CreditCards)
                {
                    CreditCardProcessor creditCardProcessor = new CreditCardProcessor(creditCard);
                    totalInterest += creditCardProcessor.CalculateInterest();
                }
            }

            return totalInterest;
        }

        public Dictionary<Wallet, decimal> GetInterestPerWallet()
        {
            Dictionary<Wallet, decimal> walletToInterest = new Dictionary<Wallet, decimal>();

            foreach (Wallet customerWallet in _customer.Wallets)
            {
                decimal walletInterest = 0m;

                foreach (ICreditCard creditCard in customerWallet.CreditCards)
                {
                    CreditCardProcessor creditCardProcessor = new CreditCardProcessor(creditCard);
                    walletInterest += creditCardProcessor.CalculateInterest();
                }

                walletToInterest.Add(customerWallet, walletInterest);
            }

            return walletToInterest;
        }
    }
}
