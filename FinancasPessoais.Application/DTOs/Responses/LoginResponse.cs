using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expiracao")]
        public DateTime TokenExpirationTime { get; set; }
    }
}
