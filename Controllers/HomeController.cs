using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallet.Data;
using Wallet.Models.TransactionViewModels;

namespace Wallet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {

            var transactionViewModel = await _context.Transactions.OrderBy(x => x.DateTime).Select(m => new TransactionViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Amount = m.Amount,
                DateTime = m.DateTime,
                BankAccountId = m.BankAccountId,
                Latitude = m.Latitude,
                Longitude = m.Longitude
            }).ToListAsync<TransactionViewModel>();

            return View(transactionViewModel);
        }

        public IActionResult CreateBankAccount()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
