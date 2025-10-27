using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "O token é obrigatório.")]
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [Required(ErrorMessage = "O refreshToken é obrigatório.")]
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
