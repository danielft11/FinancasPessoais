using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public class AccountPayableResponseDTO
    {
        [JsonPropertyName("accountPayableId")]
        public Guid Id { get; set; }

        [JsonPropertyName("data_vencimento")]
        public DateTime DueDate { get; private set; }

        [JsonPropertyName("valor")]
        public decimal Value { get; private set; }

        [JsonPropertyName("subcategoryId")]
        public Guid SubcategoryId { get; set; }

        [JsonPropertyName("subcategoria")]
        public string Subcategory { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; private set; }

        [JsonPropertyName("cod_barras")]
        public string BarCode { get; private set; }

        [JsonPropertyName("data_lembrete")]
        public DateTime ScheduleDate { get; set; }

        [JsonPropertyName("emails")]
        public string Emails { get; set; }

        [JsonPropertyName("caminho_arquivo")]
        public string FilePath { get; set; }
    }
}
