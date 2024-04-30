#region IMPORTS
using Alfasoft.Models;
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
            // Carregue o conteúdo do README.md aqui
            var readmeContent = System.IO.File.ReadAllText("C:\\Users\\Frederico\\Downloads\\fredericorook-net\\src\\README.MD");
            return View(model: readmeContent);
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

//C: \Users\Frederico\Downloads\fredericorook-net\src\README.MD