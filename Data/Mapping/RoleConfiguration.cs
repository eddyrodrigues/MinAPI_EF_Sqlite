using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Models;

namespace Data.Mapping{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .Metadata
                .SetIdentityIncrement(1);
                

            builder.Property(t => t.Nome)
                .HasColumnType("varchar")
                .HasMaxLength(100);

    }
    }
}