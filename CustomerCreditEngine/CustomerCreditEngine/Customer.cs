using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCreditEngine
{
    public class Customer
    {
        public string CustomerID { get; private set; }
        public List<Wallet> Wallets { get; private set; }

        public Customer(string customerId, List<Wallet> wallets)
        {
            CustomerID = customerId;
            Wallets = wallets;
        }
    }
}
