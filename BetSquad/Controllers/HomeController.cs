using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BetSquad.Models;
using Microsoft.AspNetCore.Authorization;

namespace BetSquad.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Common");
        }
        public IActionResult Error(string error)
        {
            return View(new ErrorModel() { Error = error});
        }
    }
}
