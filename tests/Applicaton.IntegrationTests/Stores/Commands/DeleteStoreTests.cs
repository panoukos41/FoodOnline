using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Stores.Commands.DeleteStore;
using FoodOnline.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.Stores.Commands
{
    using static Testing;

    public class DeleteStoreTests : TestBase
    {
        [Test]
        public void ShouldRequireValidStoreId()
        {
            var command = new DeleteStoreCommand("someID");

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteStore()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);

            await SendAsync(new DeleteStoreCommand(storeId));

            var store = await FindAsync<Store>(storeId);

            store.Should().BeNull();
        }

        [Test]
        public async Task ShouldDeleteStoreAndEmployes()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);
            var employee1 = await GetRandomEmployee(storeId);
            var employee2 = await GetRandomEmployee(storeId);

            await SendAsync(new DeleteStoreCommand(storeId));

            var store = await FindAsync<Store>(storeId);
            var employess = await FindManyAsync<StoreEmployee>(employee1, employee2);

            store.Should().BeNull();
            employess.Should().HaveCount(0);
        }
    }
}