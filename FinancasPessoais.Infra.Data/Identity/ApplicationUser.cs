using Microsoft.AspNetCore.Identity;
using System;

namespace FinancasPessoais.Infra.Data.Identity
{
    public class ApplicationUser : IdentityUser {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
