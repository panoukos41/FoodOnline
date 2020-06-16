using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Domain.ValueObjects
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class AddressInvalidException : Exception
    {
        public AddressInvalidException(string address, Exception ex) : base($"Address \"{address}\" is invalid.", ex)
        {
        }
    }
}