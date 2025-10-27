using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class CategoryResponseDTO 
    {
        [JsonPropertyName("categoryId")]
        public Guid Id { get; set; }

        [JsonPropertyName("codigo")]
        public string Code { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("releaseType")]
        public string ReleaseType { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("subcategorias")]
        public List<SubcategoryResponseDTO> Subcategories { get; set; }
    }
}
