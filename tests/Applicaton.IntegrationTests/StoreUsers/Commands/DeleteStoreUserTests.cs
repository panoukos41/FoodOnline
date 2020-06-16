using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Domain.StoreUsers.Requests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.StoreUsers.Commands
{
    using static Testing;
    public class DeleteStoreUserTests
    {
        [Test]
        public void ShouldFailNotFound()
        {
            var command = new DeleteStoreUser { Id = "asdwqedg" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldSucceed()
        {
            var id = await GetRandomOwner();

            var commad = new DeleteStoreUser { Id = id };

            FluentActions.Invoking(() =>
                SendAsync(commad)).Should().NotThrow();
        }
    }
}
