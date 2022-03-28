using System.ComponentModel.DataAnnotations;
namespace ForumOnAnyTopic.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email not entered")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
