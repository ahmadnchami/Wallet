using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsCr { get; set; }
        public DateTime DateTime { get; set; }
        public int BankAccountId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
