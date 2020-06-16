using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class PasswordInvalidException : Exception
    {
        public PasswordInvalidException(string nameofEntity, object id)
            : base($"Password \"{nameofEntity}\" ({id}) is incorrect.")
        {
        }
    }
}