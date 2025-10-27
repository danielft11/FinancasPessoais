using FinancasPessoais.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs
{
    public class SubcategoryRequestDTO
    {
        [Required(ErrorMessage = "The field codigo is required")]
        [MinLength(5, ErrorMessage = "The field codigo must be 5 characters long.")]
        [MaxLength(5, ErrorMessage = "The field codigo must be 5 characters long.")]
        [JsonPropertyName("codigo")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The field nome is required")]
        [MinLength(2, ErrorMessage = "The field name must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "The field nome allows a maximum of 50 characters.")]
        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "The field descricao must be 2 characters long.")]
        [MaxLength(250, ErrorMessage = "The field descricao allows a maximum of 250 characters.")]
        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field categoryId is required")]
        [JsonPropertyName("categoryId")]
        public Guid CategoryId { get; set; }
    }
}
