using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public String AccountId { get; set; }
        public String Bank { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateTimeStamp { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
