using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class DetailedAccountResponseDTO
    {
        #region Json Properties
        [JsonPropertyName("accountId")]
        public Guid Id { get; set; }

        [JsonPropertyName("banco")]
        public string Name { get; set; }

        [JsonPropertyName("agencia")]
        public string BankBranch { get; set; }

        [JsonPropertyName("conta")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("saldo")]
        public decimal Balance { get; set; }

        [JsonPropertyName("totalReceitas")]
        public decimal TotalIncome { get; set; }

        [JsonPropertyName("totalDespesas")]
        public decimal TotalExpenses { get; set; }

        #endregion
        
    }
}
