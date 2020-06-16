using System;
using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class AuthException : Exception
    {
        public AuthException()
            : base($"Email or password is wrong.")
        {
        }
    }
}