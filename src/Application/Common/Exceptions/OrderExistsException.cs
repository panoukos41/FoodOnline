using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class OrderExistsException : Exception
    {
        public OrderExistsException(Guid IdempotencyToken)
            : base($"Order with IdempotencyToken ({IdempotencyToken}) already exists.")
        {
        }
    }
}