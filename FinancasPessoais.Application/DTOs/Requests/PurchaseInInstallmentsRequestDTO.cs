using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class PurchaseInInstallmentsRequestDTO
    {
        [Required(ErrorMessage = "The field data_compra is required")]
        [JsonPropertyName("data_compra")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "The field descricao is required")]
        [MinLength(2, ErrorMessage = "The field codigo must be 2 characters long.")]
        [MaxLength(250, ErrorMessage = "The field codigo must be 250 characters long.")]
        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field valor_compra is required")]
        [JsonPropertyName("valor_compra")]
        public decimal PurchaseValue { get; set; }

        [Required(ErrorMessage = "The field quantidade_parcelas is required")]
        [JsonPropertyName("quantidade_parcelas")]
        public int NumberOfInstallments { get; set; }

        [Required(ErrorMessage = "The field subcategoriaId is required")]
        [JsonPropertyName("subcategoriaId")]
        public Guid SubcategoryId { get; set; }

        [Required(ErrorMessage = "The field cartaoDeCreditoId is required")]
        [JsonPropertyName("cartaoDeCreditoId")]
        public Guid CreditCardId { get; set; }
    }
}
