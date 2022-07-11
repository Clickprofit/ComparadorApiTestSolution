using System;
using System.ComponentModel.DataAnnotations;

namespace ForretasAPITester.Models
{
    public class LoginData
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginData() { }
    }
}
