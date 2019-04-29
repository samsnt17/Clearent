using CreditCardEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCreditEngine
{
    public class Wallet
    {
        public List<ICreditCard> CreditCards { get; private set; }

        public Wallet(List<ICreditCard> creditCards)
        {
            CreditCards = creditCards;
        }
    }
}
