#region IMPORTS
using Alfasoft.Models;
using Microsoft.EntityFrameworkCore;
#endregion

namespace WebApplication2
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
