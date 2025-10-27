using FinancasPessoais.Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class CategoryRequestDTO
    {
        [Required(ErrorMessage = "The codigo is required")]
        [MinLength(2, ErrorMessage = "The codigo must be 2 characters long.")]
        [MaxLength(2, ErrorMessage = "The codigo must be 2 characters long.")]
        [JsonPropertyName("codigo")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The nome is required")]
        [MinLength(2, ErrorMessage = "The nome must be 2 characters long.")]
        [MaxLength(50, ErrorMessage = "The nome allows a maximum of 50 characters.")]
        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field tipo is required")]
        [JsonPropertyName("tipo")]
        public ReleaseTypes Type { get; set; }

        [MinLength(2, ErrorMessage = "The descricao must be 2 characters long.")]
        [MaxLength(250, ErrorMessage = "The descricao allows a maximum of 250 characters.")]
        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }
}
