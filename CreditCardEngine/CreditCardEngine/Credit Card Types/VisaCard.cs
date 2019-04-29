using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardEngine
{
    public class VisaCard : ICreditCard
    {
        public decimal InterestRate => .10m;

        public decimal Balance { get; set; }

        public CreditCardStatic.InterestTypes InterestType => CreditCardStatic.InterestTypes.Simple;

        public VisaCard(decimal balance)
        {
            Balance = balance;
        }
    }
}