using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardEngine
{
    public interface ICreditCard
    {
        decimal InterestRate { get; }

        decimal Balance { get; set; }

        CreditCardStatic.InterestTypes InterestType { get; }
    }
}