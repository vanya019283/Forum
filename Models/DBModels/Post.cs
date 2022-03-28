using System;
using System.ComponentModel.DataAnnotations;

namespace ForumOnAnyTopic.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Display(Name = "Message")]
        [Required(ErrorMessage = "Message is empty!!!")]
        public string Massage { get; set; }
        public DateTime CreatDate { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
