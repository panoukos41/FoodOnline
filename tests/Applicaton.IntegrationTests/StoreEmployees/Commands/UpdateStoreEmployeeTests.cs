using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.StoreEmployees.Commands.CreateEmployee;
using FoodOnline.Application.StoreEmployees.Commands.UpdateEmployee;
using FoodOnline.Domain;
using FoodOnline.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.StoreEmployees.Commands
{
    using static Testing;

    public class UpdateStoreEmployeeTests
    {
        //TODO
        [Test]
        public void ShouldRequireValidId()
        {
            var command = new UpdateStoreEmployeeCommand("aaa", "aaaaaa");

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireValidFields()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);
            var employeeId = await SendAsync(new CreateStoreEmployeeCommand(storeId, "pedro", "Aforstore:pedro", "myPassword"));

            var command = new UpdateStoreEmployeeCommand(employeeId, "myPassword", "", "", "aaa");

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireValidPassword()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);
            var employeeId = await SendAsync(new CreateStoreEmployeeCommand(storeId, "pedro", "forstore:pedro", "myPassword"));

            var command = new UpdateStoreEmployeeCommand(employeeId, "wrongPassword");

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<PasswordInvalidException>();
        }

        [Test]
        public async Task ShouldUpdatePassword()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);
            var employeeId = await SendAsync(new CreateStoreEmployeeCommand(storeId, "pedro", "top:pedro", "myPassword"));

            await SendAsync(new UpdateStoreEmployeeCommand(employeeId, "myPassword", newPassword: "myNewPassword"));

            var employee = await FindAsync<StoreEmployee>(employeeId);

            var result = PasswordHelper.VerifyPassword(
                "myNewPassword",
                employee.PasswordHash,
                employee.PasswordSalt);

            result.Should().BeTrue();
        }

        [Test]
        public async Task ShouldUpdateEmployee()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);
            var employeeId = await SendAsync(new CreateStoreEmployeeCommand(storeId, "pedro", "forMystore:pedro", "myPassword"));

            await SendAsync(new UpdateStoreEmployeeCommand(employeeId, "myPassword", "panos", "panos:atstore"));

            var employee = await FindAsync<StoreEmployee>(employeeId);

            employee.Name.Should().Be("panos");
            employee.Username.Should().Be("panos:atstore");
        }
    }
}