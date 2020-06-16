using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Domain.StoreUsers.Requests;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.StoreUsers.Commands
{
    using static Testing;

    public class UpdateStoreUserTests
    {
        [Test]
        public void ShouldFailNotFound()
        {
            var command = new UpdateStoreUser { Id = "asdwqedg" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateAsync()
        {
            var id = await GetRandomOwner();

            var commad = new UpdateStoreUser { Id = id, NewUsername = "mynewusername", Password = "aReallyStrongpassword", NewPassword = "weakPass" };

            FluentActions.Invoking(() =>
                SendAsync(commad)).Should().NotThrow();
        }
    }
}