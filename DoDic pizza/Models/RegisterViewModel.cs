﻿namespace DoDic_pizza.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Birthdate { get; set; } // Важно следить за форматом даты
    }

}
