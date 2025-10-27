using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class CreateUserRequest : LoginRequest
    {
        [Required(ErrorMessage = "O username é obrigatório.")]
        [JsonPropertyName("username")]
        public string Username { get; set; }
    }

}
