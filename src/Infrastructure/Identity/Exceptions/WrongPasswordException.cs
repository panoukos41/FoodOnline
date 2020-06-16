using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Infrastructure.Identity.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class WrongPasswordException : FoodOnlineUserException
    {
        public WrongPasswordException() : base("The provided password is wrong!")
        {
        }
    }
}