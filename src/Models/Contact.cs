using System.ComponentModel.DataAnnotations;

namespace Alfasoft.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$")]
        public string Number { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
