using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Infrastructure.Identity.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class EmailTakenException : FoodOnlineUserException
    {
        public EmailTakenException(string email) : base(email + " is already taken.")
        {
        }
    }
}