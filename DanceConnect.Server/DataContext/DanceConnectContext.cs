using DanceConnect.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DanceConnect.Server.DataContext
{
    public class DanceConnectContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DanceConnectContext(DbContextOptions<DanceConnectContext> options): base(options){ }

        public DbSet<User> Users { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
    }
}
