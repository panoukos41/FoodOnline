using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class EmailExistsException : Exception
    {
        public EmailExistsException(string entityName, object email)
            : base($"Entity \"{entityName}\" ({email}) already exists.")
        {
        }
    }
}