using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardEngine
{
    public class DiscoverCard : ICreditCard
    {
        public decimal InterestRate => .01m;

        public decimal Balance { get; set; }

        public CreditCardStatic.InterestTypes InterestType => CreditCardStatic.InterestTypes.Simple;

        public DiscoverCard(decimal balance)
        {
            Balance = balance;
        }
    }
}