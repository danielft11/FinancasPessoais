using FinancasPessoais.Api.Token;
using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Infra.Data.Identity.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using FinancasPessoais.Application.Interfaces;

namespace FinancasPessoais.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;

        public AuthenticationController(IConfiguration configuration, IAuthenticationService authenticationService, IEmailService emailService)
        {
            _configuration = configuration;
            
            _authenticationService = authenticationService;
            _emailService = emailService;

            TokenService.Bootstrap(_configuration);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var user = await _authenticationService.FindUserByEmail(request.Email);

            bool result = await _authenticationService.Login(request.Email, request.Password);
            
            if (user != null && result)
            {

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, request.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = TokenService.GenerateToken(claims);
                var refreshToken = TokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpirationTime = DateTime.Now.AddMinutes(60);

                await _authenticationService.UpdateUser(user);

                return new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    TokenExpirationTime = token.ValidTo
                };
            }
            else
            {
                return BadRequest("Tentativa de login inválida: e-mail ou senha incorretos.");
            }
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout() 
        {
            await _authenticationService.Logout();
            return Ok("Logout realizado com sucesso!");
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest userInfo)
        {
            var userExists = await _authenticationService.FindUserByEmail(userInfo.Email);
            if (userExists != null) 
                return BadRequest("Este usuário já existe.");

            var result = await _authenticationService.RegisterUser(userInfo.Email, userInfo.Password);
            if (result)
                return Ok($"Usuário {userInfo.Email} criado com sucesso!");
            
            return BadRequest("Falha ao criar o usuário com os dados solicitados.");
            
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request) 
        {
            var principal = TokenService.GetPrincipalFromExpiredToken(request.Token);
            if (principal == null)
                return BadRequest("Token ou refresh token inválidos.");

            var user = await _authenticationService.FindUserByName(principal.Identity.Name);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpirationTime <= DateTime.Now)
                return BadRequest("Token ou refresh token inválidos.");

            var newToken = TokenService.GenerateToken(principal.Claims.ToList());
            var newRefreshToken = TokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpirationTime = DateTime.Now.AddMinutes(60);

            await _authenticationService.UpdateUser(user);

            return new RefreshTokenResponse
            {
                NewToken = new JwtSecurityTokenHandler().WriteToken(newToken),
                NewRefreshToken = newRefreshToken
            };

        }

        [HttpPost]
        [Route("Revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _authenticationService.FindUserByName(username);
            if (user == null) 
                return BadRequest("Nome de usuário inválido.");

            user.RefreshToken = null;

            await _authenticationService.UpdateUser(user);
            
            return Ok($@"Refresh Token do usuário {username} revogado com sucesso!");
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = await _authenticationService.FindUserByEmail(request.Email);
            if (user == null)
                return BadRequest(); 

            var token = await _authenticationService.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var callbackUrl = $"{_configuration["Frontend:ResetPasswordUrl"]}?email={request.Email}&token={encodedToken}";

            _emailService.SendResetPasswordEmail(request.Email, callbackUrl);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _authenticationService.FindUserByEmail(request.Email);
            if (user == null)
                return BadRequest("Usuário não encontrado.");

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _authenticationService.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        #region Documentação
        //https://macoratti.net/22/06/aspnc_jwtrfsh2.htm
        #endregion

    }
}
