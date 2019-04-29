using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardEngine
{
    public class CreditCardProcessor
    {
        private ICreditCard _card;

        public CreditCardProcessor(ICreditCard card)
        {
            _card = card;
        }

        public decimal CalculateInterest()
        {
            switch (_card.InterestType)
            {
                case CreditCardStatic.InterestTypes.Simple:
                    return CalculateSimpleInterest(_card.InterestRate, _card.Balance);

                default:
                    throw new Exception(string.Format("Error calculating interest - Unhandled interest type encountered: [{0}].", _card.InterestType));
            }
        }

        private decimal CalculateSimpleInterest(decimal interestRate, decimal balance)
        {
            if (interestRate <= 0)
            {
                throw new Exception(string.Format("Unable to calculate simple interest. Invalid interest rate [{0}] encountered for card type [{1}].", interestRate, nameof(_card)));
            }

            return interestRate * balance;
        }
    }
}