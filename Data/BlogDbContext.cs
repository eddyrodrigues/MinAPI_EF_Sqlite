using Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data {

    public class BlogDbContext: DbContext{
        
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=Category.db;Cache=Shared");

    }

}