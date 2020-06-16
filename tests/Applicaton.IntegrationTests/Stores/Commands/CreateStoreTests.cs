using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Stores.Commands.CreateStore;
using FoodOnline.Domain.Entities;
using FoodOnline.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.Stores.Commands
{
    using static Testing;

    public class CreateStoreTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var ownerId = await GetRandomOwner();
            var bigName = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

            var command = new CreateStoreCommand(ownerId, bigName, new Address("", "", "", "", ""));

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            var ownerId = await GetRandomOwner();

            await SendAsync(new CreateStoreCommand(
                storeOwnerId: ownerId,
                name: "A good store",
                address: new Address("", "", "", "", "")));

            var command = new CreateStoreCommand(
                storeOwnerId: ownerId,
                name: "A good store",
                address: new Address("", "", "", "", ""));

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateStore()
        {
            var ownerId = await GetRandomOwner();

            var storeId = await SendAsync(new CreateStoreCommand(
                storeOwnerId: ownerId,
                name: "A very good store",
                address: new Address("", "", "", "", "")));

            var store = await FindAsync<Store>(storeId);

            store.Should().NotBeNull();
            store.Name.Should().Be("A very good store");
        }
    }
}