using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class RefreshTokenResponse
    {
        [JsonPropertyName("newToken")]
        public string NewToken { get; set; }

        [JsonPropertyName("newRefreshToken")]
        public string NewRefreshToken { get; set; }
    }
}
