using System.Diagnostics;
using Imtahan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Imtahan.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
     
    }
}
