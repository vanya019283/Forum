using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumOnAnyTopic.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Display(Name = "Topic name")]
        [Required(ErrorMessage = "Topic name is empty!!!")]
        public string Name { get; set; }
        [Display(Name = "Topic description")]
        [Required(ErrorMessage = "Topic description is empty!!!")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public List<Post> Posts { get; set; }
        public Topic()
        {
            Posts = new List<Post>();
        }
    }
}
