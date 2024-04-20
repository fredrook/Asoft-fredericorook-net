#region IMPORTS
using Alfasoft.Models;
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
            var existingContact = await _context.Contacts.FindAsync(contact.Id);
            if (existingContact == null)
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
