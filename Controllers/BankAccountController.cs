using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wallet.Data;
using Wallet.Models.BankAccountViewModels;
using Wallet.Models;
using Microsoft.AspNetCore.Authorization;

namespace Wallet.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("account")]
        // GET: BankAccount
        public async Task<IActionResult> Index()
        {

            var bankAccountViewModel = await _context.Accounts.Select(m => new BankAccountViewModel()
            {
                Id = m.Id,
                AccountNumber = m.AccountId,
                Bank = m.Bank,
                Balance = m.Balance
            }).ToListAsync<BankAccountViewModel>();

            return View(bankAccountViewModel);
        }

        // GET: BankAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountViewModel = await _context.Accounts
                .Where(m => m.Id == id)
                .Select(m => new BankAccountViewModel()
                {
                    Id = m.Id,
                    AccountNumber = m.AccountId,
                    Bank = m.Bank,
                    Balance = m.Balance
                }).FirstAsync<BankAccountViewModel>();

            if (bankAccountViewModel == null)
            {
                return NotFound();
            }

            return View(bankAccountViewModel);
        }

        // GET: BankAccount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccountViewModel bankAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                BankAccount account = new BankAccount()
                {
                    AccountId = bankAccountViewModel.AccountNumber,
                    Bank = bankAccountViewModel.Bank,
                    Balance = bankAccountViewModel.Balance,
                    DateTimeStamp = DateTime.UtcNow
                };

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bankAccountViewModel);
        }

        // GET: BankAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bankAccountViewModel = await _context.Accounts
                .Where(m => m.Id == id)
                .Select(m => new BankAccountViewModel()
                {
                    Id = m.Id,
                    AccountNumber = m.AccountId,
                    Bank = m.Bank,
                    Balance = m.Balance
                }).FirstAsync<BankAccountViewModel>();

            if (bankAccountViewModel == null)
            {
                return NotFound();
            }
            return View(bankAccountViewModel);
        }

        // POST: BankAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,Bank,Balance")] BankAccountViewModel bankAccountViewModel)
        {
            if (id != bankAccountViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    BankAccount account = new BankAccount()
                    {
                        Id = bankAccountViewModel.Id,
                        AccountId = bankAccountViewModel.AccountNumber,
                        Bank = bankAccountViewModel.Bank,
                        Balance = bankAccountViewModel.Balance,
                        DateTimeStamp = DateTime.UtcNow
                    };

                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccountViewModelExists(bankAccountViewModel.Id))
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
            return View(bankAccountViewModel);
        }

        // GET: BankAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccountViewModel = await _context.Accounts
                .Where(m => m.Id == id)
                .Select(m => new BankAccountViewModel()
                {
                    Id = m.Id,
                    AccountNumber = m.AccountId,
                    Bank = m.Bank,
                    Balance = m.Balance
                }).FirstAsync<BankAccountViewModel>();

            if (bankAccountViewModel == null)
            {
                return NotFound();
            }

            return View(bankAccountViewModel);
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var account = await _context.Accounts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BankAccountViewModelExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
