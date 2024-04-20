#region IMPORTS
using Alfasoft.Models;
using Alfasoft.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2;
namespace Alfasoft.Controllers;
#endregion

public class PeopleController : Controller

{
    private readonly ApplicationDbContext _context;
    private readonly PersonService _personService;
    private readonly ContactService _contactService;
    private readonly CountriesService _countriesService;

    public PeopleController(ApplicationDbContext context, PersonService personService, ContactService contactService, CountriesService countriesService)
    {
        _context = context;
        _personService = personService;
        _contactService = contactService;
        _countriesService = countriesService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SaveContact(Contact contact)
    {
        if (ModelState.IsValid)
        {
            await _contactService.AddOrUpdateContactAsync(contact);
            return RedirectToAction("Index");
        }
        return View("AddOrEditContact", contact);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var people = await _personService.GetAllPeopleAsync();
        return View(people);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        if (person == null)
            return NotFound();
        return View(person);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOrEditContact(int? id)
    {
        var countries = await _countriesService.GetCountriesAsync();
        ViewBag.Countries = new SelectList(countries, "Code", "Name");
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> CreateOrEdit(int? id)
    {
        if (id == null)
            return View(new Person());
        else
        {
            var person = await _personService.GetPersonByIdAsync(id.Value);
            if (person == null)
                return NotFound();
            return View(person);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrEdit(Person person)
    {
        if (ModelState.IsValid)
        {
            await _personService.AddOrUpdatePersonAsync(person);
            return RedirectToAction(nameof(Index));
        }
        return View(person);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _personService.DeletePersonAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
