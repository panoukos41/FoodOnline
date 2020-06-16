using FoodOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOnline.Infrastructure.Persistence.Configurations
{
    public class StoreUserConfiguration : IEntityTypeConfiguration<StoreUser>
    {
        public void Configure(EntityTypeBuilder<StoreUser> builder)
        {
            builder.Property(x => x.Username)
                .HasMaxLength(51)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();
            builder.Property(x => x.PasswordSalt)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasMany(x => x.Stores)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.OwnerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Store)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.StoreId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}