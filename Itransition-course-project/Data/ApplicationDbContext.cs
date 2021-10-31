using System;
using System.Linq;
using Itransition_course_project.Models;
using Itransition_course_project.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Itransition_course_project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<RateUserPost> RateUserPosts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            TopicsEnsureCreated();
        }

        private void TopicsEnsureCreated()
        {
            if (!Topics.Any())
            {
                Topics.Add(new Topic(){Name = "Basics"});
                Topics.Add(new Topic(){Name = ".Net"});
                Topics.Add(new Topic(){Name = "JavaScript"});
                SaveChanges();
            }
        }
    }
}