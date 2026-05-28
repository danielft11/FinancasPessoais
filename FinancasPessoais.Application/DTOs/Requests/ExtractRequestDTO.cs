using System;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class ExtractRequestDTO
    {
        [JsonPropertyName("accountID")]
        public Guid? AccountID { get; set; }

        [JsonPropertyName("period")]
        public int Period { get; set; }
    }
}
