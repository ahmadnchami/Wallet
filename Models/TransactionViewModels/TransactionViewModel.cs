using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Models.TransactionViewModels
{
    public class TransactionViewModelList
    {
        public List<TransactionViewModel> List { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalDr
        {
            get
            {
                return List.Where(x => x.IsCr == false).Sum(m => m.Amount);
            }
        }
        [DataType(DataType.Currency)]
        public decimal TotalCr
        {
            get
            {
                return List.Where(x => x.IsCr).Sum(m => m.Amount);
            }
        }

        public Boolean Filtered { get; set; }

        public TransactionFilter Filter { get; set; }
    }
    public class TransactionViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal _amount;
        [DataType(DataType.Currency)]
        public decimal Amount
        {
            get; set;
        }

        public int BankAccountId { get; set; }
        public bool IsCr { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public ParsedDateTime ParsedDateTime
        {
            get
            {
                return new ParsedDateTime()
                {
                    Year = this.DateTime.Year,
                    Month = this.DateTime.Month,
                    Day = this.DateTime.Day
                };
            }
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class ParsedDateTime
    {
        public Int32 Year { get; set; }
        public Int32 Month { get; set; }
        public Int32 Day { get; set; }

    }

    public class TransactionFilter
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public bool Active { get; set; }
    }
}
