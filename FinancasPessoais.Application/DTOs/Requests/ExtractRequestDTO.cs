using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class ExtractRequestDTO
    {
        [Required(ErrorMessage = "The field accountID is required")]
        [JsonPropertyName("accountID")]
        public Guid AccountID { get; set; }

        [JsonPropertyName("period")]
        public int Period { get; set; }
    }
}
