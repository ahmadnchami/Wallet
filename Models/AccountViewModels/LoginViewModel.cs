using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wallet.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public String Username { get; set; }
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 chars")]
        public String Password { get; set; }
        public Boolean RememberMe { get; set; }
    }
}
