#region IMPORTS
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
#endregion

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;


        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            return Json(_dbContext.Customers);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}