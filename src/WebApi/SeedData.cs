using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOnline.WebApi
{
    public class SeedData
    {
        private static readonly Random Random = new Random();

        public static async Task Initialize(IApplicationDbContext context)
        {
            if (context.Stores.Count() != 0) return;

            var owners = CreateSeedOwners().ToList();
            var stores = CreateSeedStores(owners).ToList();

            context.StoreUsers.AddRange(owners);
            context.Stores.AddRange(stores);

            await context.SaveChangesAsync(default);
        }

        private static IEnumerable<StoreUser> CreateSeedOwners()
        {
            PasswordHelper.CreatePasswordHash("owner1password", out string hash1, out string salt1);
            yield return new StoreUser
            {
                Id = IdGenerator.Generate(),
                Username = "owner1",
                PasswordHash = hash1,
                PasswordSalt = salt1,
                Role = Role.StoreOwner,
            };

            PasswordHelper.CreatePasswordHash("owner2password", out string hash2, out string salt2);
            yield return new StoreUser
            {
                Id = IdGenerator.Generate(),
                Username = "owner2",
                PasswordHash = hash2,
                PasswordSalt = salt2,
                Role = Role.StoreOwner,
            };

            PasswordHelper.CreatePasswordHash("adminpassword", out string hash3, out string salt3);
            yield return new StoreUser
            {
                Id = IdGenerator.Generate(),
                Username = "admin",
                PasswordHash = hash3,
                PasswordSalt = salt3,
                Role = Role.StoreOwner,
            };
        }

        private static IEnumerable<Store> CreateSeedStores(IEnumerable<StoreUser> owners)
        {
            for (int i = 0; i < Names.Count(); i++)
            {
                yield return new Store
                {
                    OwnerId = PickRandom(owners).Id,
                    Name = Names[i],
                    Address = new Address("Street", PickRandom(Regions), "Αθήνα", "Ελλάδα", "123"),
                    DeliversTo = string.Join(',', PickRandomManyUnique(Regions, 3)),
                    Description = "A new description",
                    Id = IdGenerator.Generate(),
                    Open = false,
                    Published = false
                };
            }
        }

        private static string[] Names = new[] { "Store1", "Store2", "Store3", "Store4", "" };
        private static readonly string[] Regions = new[] { "Βύρωνας", "Καισαριανή", "Παγκράτι", "Ηλιούπολη", "Πειραιάς", "Υμηττός", "Κηφισιά", "Γουδί" };

        #region Helpers

        private static T PickRandom<T>(IEnumerable<T> values)
        {
            return values.ElementAt(Random.Next(values.Count()));
        }

        private static IEnumerable<T> PickRandomManyUnique<T>(IEnumerable<T> values, int numToPick)
        {
            int count = values.Count();
            return values
                .Select(x => new { value = x, rand = Random.Next(count) })
                .Distinct()
                .OrderBy(x => x.rand)
                .Take(numToPick)
                .Select(x => x.value);
        }

        private static T PickRandomEnum<T>()
        {
            return PickRandom((T[])Enum.GetValues(typeof(T)));
        }

        #endregion
    }
}