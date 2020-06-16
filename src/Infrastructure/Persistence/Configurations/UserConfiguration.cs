using FoodOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOnline.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly ValueComparer<ICollection<string>> valueComparer = new ValueComparer<ICollection<string>>(
            equalsExpression: (c1, c2) => c1.SequenceEqual(c2),
            hashCodeExpression: c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            snapshotExpression: c => c);

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.LoginProvider)
                .IsRequired();

            builder.Property(x => x.ProviderDisplayName)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.FavoriteStores)
                .HasConversion(
                    to => string.Join(',', to),
                    from => from.Split(',', StringSplitOptions.None))
                .Metadata
                    .SetValueComparer(valueComparer);
        }
    }
}