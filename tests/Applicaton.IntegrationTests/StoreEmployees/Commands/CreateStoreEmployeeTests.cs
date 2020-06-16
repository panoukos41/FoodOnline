using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.StoreEmployees.Commands.CreateEmployee;
using FoodOnline.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.StoreEmployees.Commands
{
    using static Testing;

    public class CreateStoreEmployeeTests
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);

            var command = new CreateStoreEmployeeCommand("", "", "", "");

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueUsername()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);

            await SendAsync(new CreateStoreEmployeeCommand(storeId, "pedro", "username", "aStrong"));

            var command = new CreateStoreEmployeeCommand(storeId, "alexander", "username", "aStrongPass");

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateEmployeeStore()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);

            var employeeId = await SendAsync(new CreateStoreEmployeeCommand(storeId, "pedro", "A-username", "aStrong"));

            var employee = await FindAsync<StoreEmployee>(employeeId);

            employee.Should().NotBeNull();
        }
    }
}