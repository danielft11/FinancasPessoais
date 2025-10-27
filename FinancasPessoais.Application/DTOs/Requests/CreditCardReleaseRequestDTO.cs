using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class CreditCardReleaseRequestDTO
    {
        [Required(ErrorMessage = "The field data_compra is required")]
        [JsonPropertyName("data_compra")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "The field valor_compra is required")]
        [JsonPropertyName("valor_compra")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "The field descricao is required")]
        [MinLength(2, ErrorMessage = "The field descricao must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "The field descricao allows a maximum of 250 characters.")]
        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field subcategoria is required")]
        [JsonPropertyName("subcategoria")]
        public Guid SubcategoryId { get; set; }

        [Required(ErrorMessage = "The field cartaoDeCreditoId is required")]
        [JsonPropertyName("cartaoDeCreditoId")]
        public Guid CreditCardId { get; set; }

    }
}
