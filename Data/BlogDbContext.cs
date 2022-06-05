using Blog.Models;
using Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Data {

    public class BlogDbContext: DbContext{
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=Category.db;Cache=Shared");

    }

}