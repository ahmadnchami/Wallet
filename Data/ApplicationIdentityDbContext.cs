using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallet.Models;

namespace Wallet.Data
{
    public class ApplicationIdentityDbContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            :base(options)
        {

        }
    }
}
