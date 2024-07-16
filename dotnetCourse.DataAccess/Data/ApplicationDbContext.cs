using dotnetCourse.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetCourse.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Status> statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "work", CategoryName = "Work" },
                 new Category { CategoryId = "home", CategoryName = "Home" },
                  new Category { CategoryId = "ex", CategoryName = "Exercise" },
                   new Category { CategoryId = "shop", CategoryName = "Shopping" },
                    new Category { CategoryId = "call", CategoryName = "Contact" }

                );
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", StatusName = "Open" },
                 new Status { StatusId = "closed", StatusName = "Completed" }


                );

        }
    }
}
