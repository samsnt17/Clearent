using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardEngine
{
    public class MasterCard : ICreditCard
    {
        public decimal InterestRate => .05m;

        public decimal Balance { get; set; }

        public CreditCardStatic.InterestTypes InterestType => CreditCardStatic.InterestTypes.Simple;

        public MasterCard(decimal balance)
        {
            Balance = balance;
        }
    }
}