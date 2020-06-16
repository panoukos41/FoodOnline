using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class UsernameExistsException : Exception
    {
        public UsernameExistsException(string entityName, object username)
            : base($"Entity \"{entityName}\" ({username}) already exists.")
        {
        }
    }
}