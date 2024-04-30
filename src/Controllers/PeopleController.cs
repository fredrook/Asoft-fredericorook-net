#region IMPORTS
using Alfasoft.Interface;
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
    private readonly IPersonService _personService;
    private readonly IAvatarService _avatarService;

    public PeopleController(ApplicationDbContext context, IPersonService personService, IAvatarService avatarService)
    {
        _context = context;
        _personService = personService;
        _avatarService = avatarService;
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
        {
            return NotFound();
        }
        return View(person);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> CreateOrEdit(int? id)
    {
        if (id == null)
        {
            return View(new Person());
        }
        else
        {
            var person = await _personService.GetPersonByIdAsync(id.Value);
            if (person == null)
            {
                return NotFound();
            }
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
        await _personService.SoftDeletePersonAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
