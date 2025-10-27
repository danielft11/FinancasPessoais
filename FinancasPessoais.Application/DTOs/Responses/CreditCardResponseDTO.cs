using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class CreditCardResponseDTO
    {
        [JsonPropertyName("creditCardId")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome_cartao")]
        public string CardName { get; set; }

        [JsonPropertyName("numero_cartao")]
        public string CardNumber { get; set; }

        [JsonPropertyName("limite_cartao")]
        public decimal CardLimit { get; set; }

        [JsonPropertyName("fatura_atual")]
        public decimal CurrentInvoice { get; set; }

        [JsonPropertyName("saldo_disponivel")]
        public decimal Balance { get; set; }

        [JsonPropertyName("data_fechamento_fatura")]
        public int InvoiceClosingDate { get; set; }

        [JsonPropertyName("data_vencimento_fatura")]
        public int InvoiceDueDate { get; set; }
    }
}
