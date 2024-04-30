#region IMPORTS
using Microsoft.AspNetCore.Mvc;
using WebApplication2;
#endregion

namespace Alfasoft.Controllers
{
    public class ContactStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> ContactStatistics()
        {
            var contactsByCountry = _context.Contacts
                .GroupBy(c => c.CountryCode)
                .Select(group => new { CountryCode = group.Key, Count = group.Count() })
                .ToList();

            return View("~/Views/Contact/ContactStatistics.cshtml", contactsByCountry);
        }
    }
}
