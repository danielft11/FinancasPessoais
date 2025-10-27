using FinancasPessoais.Infra.Data.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FinancasPessoais.Infra.Data.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(applicationUser, password);

            return result.Succeeded;
        }

        public async Task<ApplicationUser> FindUserByEmail(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<ApplicationUser> FindUserByName(string userName) => await _userManager.FindByNameAsync(userName);

        public async Task UpdateUser(ApplicationUser user) => await _userManager.UpdateAsync(user);

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user) => await _userManager.GeneratePasswordResetTokenAsync(user);

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string decodedToken, string newPassword) 
        {
            return await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);
        } 
        
    }

}
