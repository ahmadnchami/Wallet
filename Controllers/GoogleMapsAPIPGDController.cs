using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Wallet.Controllers
{
    public class GoogleMapsAPIPGDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}