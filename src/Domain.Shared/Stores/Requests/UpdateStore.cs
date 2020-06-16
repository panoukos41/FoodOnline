using MediatR;

namespace FoodOnline.Domain.Stores.Requests
{
    public class UpdateStore : IRequest
    {
        /// <summary>
        /// The store id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// If true the store is open for clients to order.
        /// </summary>
        public bool Open { get; set; }

        /// <summary>
        /// If true the store is published for clients to find.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// The Description of the store.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A yaml object of Catalogue items.
        /// </summary>
        public string Catalogue { get; set; }
    }
}