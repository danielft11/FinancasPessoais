using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class PurchaseInInstallmentsResponseDTO
    {
        [JsonPropertyName("purchaseInInstallmentsId")]
        public Guid Id { get; set; }

        [JsonPropertyName("data_compra")]
        public DateTime PurchaseDate { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("valor_compra")]
        public string PurchaseValue { get; set; }

        [JsonPropertyName("quantidade_parcelas")]
        public int NumberOfInstallments { get; set; }

        [JsonPropertyName("cartao_de_credito")]
        public string CreditCardName { get; set; }
    }

}
