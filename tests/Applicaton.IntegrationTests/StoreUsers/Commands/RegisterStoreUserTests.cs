using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Domain.StoreUsers.Requests;
using NUnit.Framework;

namespace FoodOnline.Application.IntegrationTests.StoreUsers.Commands
{
    using static Testing;

    public class RegisterStoreUserTests
    {
        [Test]
        public void ShouldFailValidation()
        {
            var command = new RegisterStoreUser
            {
                IsOwner = true,
                Password = "no",
                Username = "nah"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldSucceedValidation()
        {
            var command = new RegisterStoreUser
            {
                IsOwner = true,
                Password = "reallyStrong",
                Username = "SomeoneToLogIn"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }
    }
}