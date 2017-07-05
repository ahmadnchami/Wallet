using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallet.Models;
using Wallet.Models.BankAccountViewModels;
using Wallet.Models.TransactionViewModels;
using Wallet.Models.AccountViewModels;

namespace Wallet.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<BankAccount> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet.Models.TransactionViewModels.TransactionViewModel> TransactionViewModel { get; set; }
    }
}
