using System.Threading.Tasks;

namespace FoodOnline.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUsernameAsync(string userId);
    }
}