using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class SubcategoryResponseDTO
    {
        [JsonPropertyName("subcategoryId")]
        public Guid Id { get; set; }

        [JsonPropertyName("codigo")]
        public string Code { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("categoria")]
        public string Categoria { get; set; }

    }
}
