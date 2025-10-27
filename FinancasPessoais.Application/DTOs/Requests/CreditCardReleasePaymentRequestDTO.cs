using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class CreditCardReleasePaymentRequestDTO
    {
        [Required(ErrorMessage = "The field creditCardReleaseId is required")]
        [JsonPropertyName("creditCardReleaseId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field data_pagamento is required")]
        [JsonPropertyName("data_pagamento")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "The field conta is required")]
        [JsonPropertyName("conta")]
        public Guid AccountId { get; set; }
    }
}
