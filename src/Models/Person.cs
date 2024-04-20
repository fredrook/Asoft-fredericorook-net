namespace Alfasoft.Models
{    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public bool IsDeleted { get; set; }
    }
}
