using FoodOnline.Domain.Auth.Requests;
using System.Threading.Tasks;

namespace FoodOnline.WebClient.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginUser model);

        Task LogoutAsync();

        Task RegisterAsync(RegisterUser model);
    }
}