using System;
using System.ComponentModel.DataAnnotations;
namespace ForumOnAnyTopic.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email not entered")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "First name not entered")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name not entered")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Birthdate not entered")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}
