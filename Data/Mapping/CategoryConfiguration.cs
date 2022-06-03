using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Data.Mapping{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .Metadata
                .SetIdentityIncrement(1);
                

            builder.Property(t => t.Name)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(t => t.Slug)
                .HasColumnType("varchar")
                .HasMaxLength(100);

        }
    }
}