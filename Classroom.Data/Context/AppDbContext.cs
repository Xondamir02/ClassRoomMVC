using Classroom.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Data.Context
{
    public class AppDbContext:IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSchool> UserSchools { get; set; }
        public DbSet<Science> Sciences { get; set; }
        public DbSet<UserScience> UserSciences { get; set; }
        public DbSet<JoinScienceRequest> JoinScienceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);


            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("users");

            builder.Entity<User>()
                .Property(user => user.Firstname)
                .HasColumnName("firstname")
                .HasMaxLength(50)
                .IsRequired(false);    
            

            builder.Entity<User>()
                .Property(user => user.Lastname)
                .HasColumnName("lastname")
                .HasMaxLength(50)
                .IsRequired(false);        
            

            builder.Entity<User>()
                .Property(user => user.PhotoUrl)
                .HasColumnName("photourl")
                .IsRequired(false);
        }
    }
}
