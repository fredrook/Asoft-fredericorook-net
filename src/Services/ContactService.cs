#region IMPORTS
using Alfasoft.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
#endregion

namespace Alfasoft.Services
{
    public class ContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateContactAsync(Contact contact)
        {
            var existingContact = await _context.Contacts
                .Where(c => c.CountryCode == contact.CountryCode && c.Number == contact.Number && c.Id != contact.Id)
                .FirstOrDefaultAsync();

            if (existingContact != null)
            {
                throw new InvalidOperationException("A contact with the same country code and number already exists.");
            }

            if (contact.Id == 0)
            {
                _context.Contacts.Add(contact);
            }
            else
            {
                _context.Contacts.Update(contact);
            }
            await _context.SaveChangesAsync();
        }
    }
}
