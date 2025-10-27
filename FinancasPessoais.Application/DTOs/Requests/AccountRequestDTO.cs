using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class AccountRequestDTO
    {
        [Required(ErrorMessage = "The field banco is required")]
        [MinLength(3, ErrorMessage = "the field banco must be at least 3 characters long.")]
        [MaxLength(250, ErrorMessage = "The field banco allows a maximum of 250 characters.")]
        [JsonPropertyName("banco")]
        public string Name { get; set; }

        [JsonPropertyName("agencia")]
        [MaxLength(30, ErrorMessage = "The field agencia allows a maximum of 30 characters.")]
        public string BankBranch { get; set; }

        [JsonPropertyName("conta")]
        [MaxLength(30, ErrorMessage = "The field agencia allows a maximum of 30 characters.")]
        public string AccountNumber { get; set; }
    }
}
