using Microsoft.AspNetCore.Mvc;
using STAREvents.Web.Models;
using System.Diagnostics;

namespace STAREvents.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
