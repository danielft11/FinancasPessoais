using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FinancasPessoais.Infra.Data.Identity.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Login(string email, string password);
        Task<bool> RegisterUser(string email, string password);
        Task Logout();
        Task<ApplicationUser> FindUserByEmail(string email);
        Task<ApplicationUser> FindUserByName(string userName);
        Task UpdateUser(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string decodedToken, string newPassword);
    }
}
