using FoodOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOnline.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.IdempotencyToken)
                .IsRequired();

            builder.Property(x => x.Entries)
                .IsRequired();

            builder.Property(x => x.TotalPriceEur)
                .IsRequired();

            builder.Property(x => x.State)
                .IsRequired();

            builder.OwnsOne(x => x.Address);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .IsRequired(false);

            builder.HasOne(x => x.Store)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StoreId)
                .IsRequired(false);
        }
    }
}