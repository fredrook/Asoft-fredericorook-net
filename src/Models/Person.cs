using System.ComponentModel.DataAnnotations;

namespace Alfasoft.Models
{    public class Person : User
    {
        public int Id { get; set; } 

        [Required]
        [MinLength(6)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Avatar { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public bool IsDeleted { get; set; }
    }
}
