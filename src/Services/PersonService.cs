#region IMPORTS
using Alfasoft.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
#endregion

namespace Alfasoft.Services
{
    public class PersonService
    {
        private readonly ApplicationDbContext _context;
        private readonly CountriesService _countriesService;

        public PersonService(ApplicationDbContext context, CountriesService countriesService)
        {
            _context = context;
            _countriesService = countriesService;
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _context.Persons.Include(p => p.Contacts).ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.Include(p => p.Contacts)
                                         .FirstOrDefaultAsync(p => p.Id == id);
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

        internal Task<string?> GetPersonByIdAsync(object value)
        {
            throw new NotImplementedException();
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

        public class AvatarService
        {
            private static readonly HttpClient _httpClient = new HttpClient();
            public async Task<string> GetRandomAvatarUrl()
            {
                var response = await _httpClient.GetAsync("https://app.pixelencounter.com/api/basic/monsters/random/png");
                if (response.IsSuccessStatusCode)
                {
                    var avatarUrl = response.Headers.Location.ToString();
                    return avatarUrl;
                }
                return null;
            }
        }

        public async Task AddOrUpdatePersonAsync(Person person)
        {
            if (person.Id == 0)
            {
                if (string.IsNullOrEmpty(person.Avatar))
                {
                    person.Avatar = await new AvatarService().GetRandomAvatarUrl();
                }
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
