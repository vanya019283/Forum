using ForumOnAnyTopic.Models;
using Microsoft.EntityFrameworkCore;
namespace ForumOnAnyTopic.Data
{
    public class ForumDB : DbContext
    {
        public ForumDB(DbContextOptions<ForumDB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
