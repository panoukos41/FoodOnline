using System.Diagnostics.CodeAnalysis;

namespace FoodOnline.Infrastructure.Identity.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors")]
    public class FoodOnlineUserNotFoundException : FoodOnlineUserException
    {
        public FoodOnlineUserNotFoundException() : base("User not found")
        {
        }
    }
}