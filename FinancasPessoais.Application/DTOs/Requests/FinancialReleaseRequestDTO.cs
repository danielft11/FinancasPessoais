using FinancasPessoais.Domain.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class FinancialReleaseRequestDTO
    {
        [Required(ErrorMessage = "The field tipo is required")]
        [JsonPropertyName("tipo")]
        public ReleaseTypes Type { get; set; }

        [Required(ErrorMessage = "The field data_lancamento is required")]
        [JsonPropertyName("data_lancamento")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "The field valor is required")]
        [JsonPropertyName("valor")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "The field descricao is required")]
        [MinLength(2, ErrorMessage = "The field descricao must be at least 2 characters long.")]
        [MaxLength(250, ErrorMessage = "The field descricao allows a maximum of 250 characters.")]
        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field subcategoria is required")]
        [JsonPropertyName("subcategoria")]
        public Guid SubcategoryId { get; set; }

        [JsonPropertyName("conta")]
        public Guid? AccountId { get; set; }

        [JsonPropertyName("cartao_de_credito_id")]
        public Guid? CreditCardId { get; set; }
    }

}
