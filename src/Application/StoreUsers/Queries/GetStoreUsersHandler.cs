using FoodOnline.Application.Common.Interfaces;
using FoodOnline.Domain;
using FoodOnline.Domain.StoreUsers.Models;
using FoodOnline.Domain.StoreUsers.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.StoreUsers.Queries
{
    public class GetStoreUsersHandler : IRequestHandler<GetStoreUsers, IEnumerable<StoreUserModel>>
    {
        private readonly IApplicationDbContext context;

        public GetStoreUsersHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StoreUserModel>> Handle(GetStoreUsers request, CancellationToken cancellationToken)
        {
            var correctRoles = new[] { Role.StoreEmployee, Role.StoreOwner };

            if (!correctRoles.Contains(request.Role))
                throw new ArgumentException($"{nameof(request.Role)} must be {string.Join(" or ", correctRoles)}");

            return await context.StoreUsers.AsNoTracking()
                .Where(storeUser =>
                    storeUser.Role == Role.StoreEmployee
                    || storeUser.Role == Role.StoreOwner)
                .Select(storeUser => new StoreUserModel
                {
                    Id = storeUser.Id,
                    Role = storeUser.Role,
                    Username = storeUser.Username
                })
                .ToListAsync();
        }
    }
}