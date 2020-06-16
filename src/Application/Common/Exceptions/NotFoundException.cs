using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object key)
            : base($"Entity \"{entityName}\" ({key}) was not found.")
        {
        }
    }
}