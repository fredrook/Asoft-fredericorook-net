﻿namespace Alfasoft.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Number { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}