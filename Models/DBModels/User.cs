using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumOnAnyTopic.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public List<Topic> Topics { get; set; }
        public List<Post> Posts { get; set; }
        public User()
        {
            Topics = new List<Topic>();
            Posts = new List<Post>();
        }
    }
}
