using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class FinancialReleaseResponseDTO
    {
        [JsonPropertyName("tipo")]
        public string Type { get; set; }

        [JsonPropertyName("data_lancamento")]
        public DateTime ReleaseDate { get; set; }

        [JsonPropertyName("valor")]
        public decimal Value { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("subcategoria")]
        public string SubcategoryName { get; set; }

        [JsonPropertyName("conta")]
        public string AccountName { get; set; }

        [JsonPropertyName("cartao")]
        public string CardName { get; set; }

    }
}
