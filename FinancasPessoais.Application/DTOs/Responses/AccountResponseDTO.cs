using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class AccountResponseDTO
    {
        [JsonPropertyName("accountId")]
        public Guid Id { get; set; }

        [JsonPropertyName("banco")]
        public string Name { get; set; }

        [JsonPropertyName("agencia")]
        public string BankBranch { get; set; }

        [JsonPropertyName("conta")]
        public string AccountNumber { get; set; }

    }
}
