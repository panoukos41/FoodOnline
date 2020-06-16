using FluentAssertions;
using FoodOnline.Application.Common.Exceptions;
using FoodOnline.Application.StoreEmployees.Commands.CreateEmployee;
using FoodOnline.Application.StoreEmployees.Commands.DeleteEmployee;
using FoodOnline.Domain.Entities;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FoodOnline.Application.IntegrationTests.StoreEmployees.Commands
{
    using static Testing;

    public class DeleteStoreEmployeeTests
    {
        [Test]
        public void ShouldRequireValidEmployeeId()
        {
            var command = new DeleteStoreEmployeeCommand("aaa");

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteEmployee()
        {
            var ownerId = await GetRandomOwner();
            var storeId = await GetRandomStore(ownerId);

            var employeeId = await SendAsync(new CreateStoreEmployeeCommand(storeId, "Pedro", "aGoodusername", "strongPasswordAmI"));

            await SendAsync(new DeleteStoreEmployeeCommand(employeeId));

            var employee = await FindAsync<StoreEmployee>(employeeId);

            employee.Should().BeNull();
        }
    }
}