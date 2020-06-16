using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Domain.Users.Requests;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.Users.Commands
{
    using static Testing;

    public class UpdateUserTests
    {
        [Test]
        public void ShouldFailNotFound()
        {
            var command = new UpdateUser { Id = "asdwqedg" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldSucceed()
        {
            var id = await GetRandomUser();

            var commad = new UpdateUser { Id = id, NewName = "my new name!!" };

            FluentActions.Invoking(() =>
                SendAsync(commad)).Should().NotThrow();
        }
    }
}