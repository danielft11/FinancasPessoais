using FinancasPessoais.Domain.Utils;
using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class ExtractResponseDTO
    {
        [JsonPropertyName("tipo")]
        public string Type { get; set; }

        [JsonPropertyName("data_lancamento")]
        public DateTime ReleaseDate { get; set; }

        [JsonPropertyName("valor")]
        public decimal Value { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("categoria")]
        public string Category { get; set; }
        
        [JsonPropertyName("subcategoria")]
        public string Subcategory { get; set; }

    }

}
