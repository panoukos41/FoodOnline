using FoodOnline.Domain.ValueObjects;
using MediatR;

namespace FoodOnline.Domain.Stores.Requests
{
    public class RegisterStore : IRequest<string>
    {
        /// <summary>
        /// The name of the store.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The address of the store.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// The id of the owner.
        /// </summary>
        public string OwnerId { get; set; }
    }
}