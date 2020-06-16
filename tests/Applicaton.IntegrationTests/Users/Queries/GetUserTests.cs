using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Domain.Users;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.Users.Queries
{
    using static Testing;

    public class GetUserTests
    {
        [Test]
        public void ShouldFailNotFound()
        {
            var command = new GetUser { Id = "asdwqedg" };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldReturnUser()
        {
            var id = await GetRandomUser();

            var user = await SendAsync(new GetUser { Id = id });

            user.Should().NotBeNull();
        }
    }
}