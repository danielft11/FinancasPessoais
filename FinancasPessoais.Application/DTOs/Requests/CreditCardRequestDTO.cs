using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class CreditCardRequestDTO
    {
        [Required(ErrorMessage = "The field nome_cartao is required")]
        [MinLength(3, ErrorMessage = "the field nome_cartao must be at least 3 characters long.")]
        [MaxLength(30, ErrorMessage = "The field nome_cartao allows a maximum of 30 characters.")]
        [JsonPropertyName("nome_cartao")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "The field numero_cartao is required")]
        [MinLength(16, ErrorMessage = "the field numero_cartao must be at least 16 characters long.")]
        [MaxLength(16, ErrorMessage = "The field numero_cartao allows a maximum of 16 characters.")]
        [JsonPropertyName("numero_cartao")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "The field nome_cartao is required")]
        [JsonPropertyName("limite_cartao")]
        public decimal CardLimit { get; set; }

        [Required(ErrorMessage = "The field data_fechamento_fatura is required")]
        [Range(1, 31, ErrorMessage = "The field data_fechamento_fatura must be between 1 and 31.")]
        [JsonPropertyName("data_fechamento_fatura")]
        public int InvoiceClosingDate { get; set; }

        [Required(ErrorMessage = "The field data_vencimento_fatura is required")]
        [Range(1, 31, ErrorMessage = "The field data_vencimento_fatura must be between 1 and 31.")]
        [JsonPropertyName("data_vencimento_fatura")]
        public int InvoiceDueDate { get; set; }
    }
}
