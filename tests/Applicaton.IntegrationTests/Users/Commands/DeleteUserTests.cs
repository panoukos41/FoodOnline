using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Domain.Users.Requests;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.Users.Commands
{
    using static Testing;

    public class DeleteUserTests
    {
        [Test]
        public void ShouldFailNotFound()
        {
            var command = new DeleteUser { Id = "asdwqedg" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldSucceed()
        {
            var id = await GetRandomUser();

            var commad = new DeleteUser { Id = id };

            FluentActions.Invoking(() =>
                SendAsync(commad)).Should().NotThrow();
        }
    }
}