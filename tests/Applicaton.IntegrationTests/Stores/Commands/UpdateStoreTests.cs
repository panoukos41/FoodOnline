using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.Stores.Commands.UpdateStore;
using FoodOnline.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.Stores.Commands
{
    using static Testing;
    public class UpdateStoreTests
    {
        [Test]
        public void ShouldRequireValidStore()
        {
            var commad = new UpdateStoreCommand("aaa", false, "", null);

            FluentActions.Invoking(() =>
                SendAsync(commad)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireDescription()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);

            var command = new UpdateStoreCommand(storeId, false, null, null);

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldUpdate()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);

            var command = new UpdateStoreCommand(storeId, true, "A great description \nfor this store!", null);

            await SendAsync(command);

            var store = await FindAsync<Store>(storeId);

            store.Published.Should().BeTrue();
            store.Description.Should().Be("A great description \nfor this store!");
            store.Catalogue.Should().NotBeNull();
        }
    }
}
