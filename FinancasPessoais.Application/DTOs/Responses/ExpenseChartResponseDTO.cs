using System;
using System.Collections.Generic;

namespace FinancasPessoais.Application.DTOs.Responses
{
    public record ExpenseChartResponseDTO
    {
        public List<string> Labels { get; set; } = new();
        public List<ExpenseChartDatasetDTO> Datasets { get; set; } = new();
    }

    public record ExpenseChartDatasetDTO
    {
        public string Type { get; set; } = "bar";
        public string Label { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public List<decimal> Data { get; set; } = new();
    }

    public record YearMonth(int Year, int Month);
}
