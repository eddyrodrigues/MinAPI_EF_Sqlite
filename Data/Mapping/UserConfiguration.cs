using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Models;

namespace Data.Mapping{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
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

      builder.HasMany(x => x.Roles)
        .WithMany(x => x.Users)
        .UsingEntity<Dictionary<string, object>>(
          "UserRole",
          usr => usr.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("UserRoleId")
                    .HasConstraintName("FK_UserRole_User_RoleId"),
          role => role.HasOne<User>()
                      .WithMany()
                      .HasForeignKey("RoleUserId")
                      .HasConstraintName("FK_UserRole_Role_UserId")

        );
    }
    }
}