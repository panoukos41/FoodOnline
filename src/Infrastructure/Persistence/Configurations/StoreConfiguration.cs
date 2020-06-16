using FoodOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOnline.Infrastructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(x => x.Published)
                .IsRequired();

            builder.Property(x => x.Open)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(x => x.Catalogue);

            builder.OwnsOne(x => x.Address);
        }
    }
}