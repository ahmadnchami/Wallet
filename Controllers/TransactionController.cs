using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wallet.Data;
using Wallet.Models.TransactionViewModels;
using Wallet.Models;
using Microsoft.AspNetCore.Authorization;

namespace Wallet.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            //var transactionViewModel = new List<TransactionViewModel>();
            //transactionViewModel.Add(new TransactionViewModel()
            //{
            //    Id = -1,
            //    Name = "bla",
            //    Description = "dsad",
            //    Amount = 100,
            //    DateTime = DateTime.UtcNow,
            //    BankAccountId = 2

            //});


            var transactionViewModel = await _context.Transactions.OrderByDescending(x => x.DateTime).Select(m => new TransactionViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Amount = m.Amount,
                IsCr = m.IsCr,
                DateTime = m.DateTime,
                BankAccountId = m.BankAccountId,
                Latitude = m.Latitude,
                Longitude = m.Longitude
            }).ToListAsync<TransactionViewModel>();

            ViewBag.Accounts = await _context.Accounts.ToListAsync<BankAccount>();

            return View(transactionViewModel);
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            TransactionViewModel transactionViewModel = new TransactionViewModel()
            {
                Id = transaction.Id,
                Name = transaction.Name,
                Description = transaction.Description,
                Amount = transaction.Amount,
                IsCr = transaction.IsCr,
                DateTime = transaction.DateTime,
                BankAccountId = transaction.BankAccountId,
                Latitude = transaction.Latitude,
                Longitude = transaction.Longitude
            };

            if (transactionViewModel == null)
            {
                return NotFound();
            }

            return View(transactionViewModel);
        }

        [Route("transaction/create")]
        // GET: Transaction/Create
        public IActionResult Create()
        {
            var bankAccountsSelectList = _context.Accounts.ToList();
            ViewBag.BankAccounts = bankAccountsSelectList;
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("transaction/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            if (ModelState.IsValid)
            {
                var transaction = new Transaction()
                {
                    Name = transactionViewModel.Name,
                    Description = transactionViewModel.Description,
                    Amount = transactionViewModel.IsCr ? transactionViewModel.Amount : transactionViewModel.Amount * -1,
                    IsCr = transactionViewModel.IsCr,
                    DateTime = transactionViewModel.DateTime,
                    BankAccountId = transactionViewModel.BankAccountId,
                    Latitude = transactionViewModel.Latitude,
                    Longitude = transactionViewModel.Longitude
                };

                _context.Add(transaction);

                var account = _context.Accounts.SingleOrDefault<BankAccount>(m => m.Id == transactionViewModel.BankAccountId);
                if (account == null) return NotFound();

                account.Balance = account.Balance + transaction.Amount;
                //account.Balance = handleTransactionAmount(account.Balance, transactionViewModel.Amount, transactionViewModel.IsCr);
                _context.Update(account);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transactionViewModel);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            TransactionViewModel transactionViewModel = new TransactionViewModel()
            {
                Id = transaction.Id,
                Name = transaction.Name,
                Description = transaction.Description,
                Amount = transaction.Amount,
                IsCr = transaction.IsCr,
                DateTime = transaction.DateTime,
                BankAccountId = transaction.BankAccountId,
                Latitude = transaction.Latitude,
                Longitude = transaction.Longitude
            };

            return View(transactionViewModel);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TransactionViewModel transactionViewModel)
        {
            if (id != transactionViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var transaction = _context.Transactions.SingleOrDefault<Transaction>(m => m.Id == transactionViewModel.Id);
                    if (transaction == null) return NotFound();


                    var account = _context.Accounts.SingleOrDefault<BankAccount>(m => m.Id == transactionViewModel.BankAccountId);
                    if (account == null) return NotFound();

                    //reverse transaction
                    account.Balance = account.Balance - transaction.Amount;
                    //account.Balance = handleTransactionAmount(account.Balance, transaction.Amount, !transaction.IsCr);

                    transaction.Name = transactionViewModel.Name;
                    transaction.Description = transactionViewModel.Description;
                    transaction.Amount = transactionViewModel.Amount;
                    transaction.DateTime = transactionViewModel.DateTime;
                    transaction.Latitude = transactionViewModel.Latitude;
                    transaction.Longitude = transactionViewModel.Longitude;

                    account.Balance = account.Balance + transactionViewModel.Amount;
                    //account.Balance = handleTransactionAmount(account.Balance, transaction.Amount, transaction.IsCr);

                    _context.Update(account);
                    _context.Update(transaction);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionViewModelExists(transactionViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(transactionViewModel);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            TransactionViewModel transactionViewModel = new TransactionViewModel()
            {
                Id = transaction.Id,
                Name = transaction.Name,
                Description = transaction.Description,
                Amount = transaction.Amount,
                IsCr = transaction.IsCr,
                DateTime = transaction.DateTime,
                BankAccountId = transaction.BankAccountId
            };

            return View(transactionViewModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(m => m.Id == id);
            var account = _context.Accounts.SingleOrDefault<BankAccount>(m => m.Id == transaction.BankAccountId);


            if (transaction == null || account == null)
            {
                return NotFound();
            }

            account.Balance = account.Balance - transaction.Amount;
            //account.Balance = handleTransactionAmount(account.Balance, transaction.Amount, !transaction.IsCr);

            _context.Accounts.Update(account);
            _context.Transactions.Remove(transaction);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("transaction/map")]
        public async Task<IActionResult> Map()
        {

            var transactionViewModel = await _context.Transactions.OrderByDescending(x => x.DateTime).Select(m => new TransactionViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Amount = m.Amount,
                IsCr = m.IsCr,
                DateTime = m.DateTime,
                BankAccountId = m.BankAccountId,
                Latitude = m.Latitude,
                Longitude = m.Longitude
            }).ToListAsync<TransactionViewModel>();

            return View(transactionViewModel);
        }

        private decimal handleTransactionAmount(decimal balance, decimal amount, bool isCr)
        {
            return isCr ? balance + amount : balance - amount;
        }

        private bool TransactionViewModelExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
