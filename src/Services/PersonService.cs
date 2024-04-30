#region IMPORTS
using Alfasoft.Interface;
using Alfasoft.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
using static Alfasoft.Services.PersonService;
#endregion

namespace Alfasoft.Services
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;
        private readonly CountriesService _countriesService;
        private readonly IAvatarService _avatarService;


        public PersonService(ApplicationDbContext context, CountriesService countriesService, IAvatarService avatarService)
        {
            _context = context;
            _countriesService = countriesService;
            _avatarService = avatarService;
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _context.Persons.Where(p => !p.IsDeleted).Include(p => p.Contacts).ToListAsync();
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.Persons
                .Where(p => !p.IsDeleted)
                .Include(p => p.Contacts)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SoftDeletePersonAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                person.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddOrUpdatePersonAsync(Person person)
        {
            var existingPerson = await _context.Persons
                .Where(p => p.Email == person.Email && p.Id != person.Id)
                .FirstOrDefaultAsync();

            if (existingPerson != null)
            {
                throw new InvalidOperationException("Another person with the same email already exists.");
            }

            if (person.Id == 0)
            {
                person.Avatar = await _avatarService.GetRandomAvatarUrl();
                _context.Persons.Add(person);
            }
            else
            {
                _context.Persons.Update(person);
            }
            await _context.SaveChangesAsync();
        }
    }
}
