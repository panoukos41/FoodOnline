using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Infrastructure.Identity.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class NoPasswordException : FoodOnlineUserException
    {
        public NoPasswordException() : base("Password is required")
        {
        }
    }
}