using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class CreditCardReleaseResponseDTO
    {
        [JsonPropertyName("data_compra")]
        public DateTime ReleaseDate { get; set; }
        
        [JsonPropertyName("data_pagamento")]
        public DateTime? PaymentDate { get; set; }

        [JsonPropertyName("valor")]
        public string Value { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("categoria")]
        public string CategoryName { get; set; }

        [JsonPropertyName("subcategoria")]
        public string SubcategoryName { get; set; }

    }

}
