using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Models.BankAccountViewModels
{
    public class BankAccountViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Account number is maxium 10 characters")]
        public String AccountNumber { get; set; }
        [Required]
        public String Bank { get; set; }
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
    }
}
