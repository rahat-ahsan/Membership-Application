using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Memberships.Entities;
using System;

namespace Memberships.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public bool IsActive { get; set; }

        public DateTime Registered { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Subscription> Subcriptions { get; set; }

        public DbSet<ProductContent> ProductContents { get; set; }
        public DbSet<UserSubscription> UserSubcriptions { get; set; } 

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}