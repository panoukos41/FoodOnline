using FoodOnline.Domain.ValueObjects;
using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class StoreExistsException : Exception
    {
        public StoreExistsException(string storename, Address address)
            : base($"Store \"{storename}\", at \"{address}\" already exists.")
        {
        }
    }
}