using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class MonthlyExtractRequestDTO
    {
        [Required(ErrorMessage = "The field accountID is required")]
        [JsonPropertyName("accountID")]
        public Guid AccountID { get; set; }

        [Required(ErrorMessage = "The field year is required")]
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "The field month is required")]
        [JsonPropertyName("month")]
        public int Month { get; set; }

        public DateTime firstDay; 
        public DateTime lastDay;

        public MonthlyExtractRequestDTO(Guid accountID, int year, int month)
        {
            AccountID = accountID;
            Year = year;
            Month = month;

            if (!IsValidYearMonth(Year, Month))
                throw new InvalidRequestException("O ano e/ou o mês fornecidos não são válidos.");

            firstDay =  new DateTime(Year, Month, 1);
            lastDay = firstDay.AddMonths(1).AddDays(-1);
        }

        private static bool IsValidYearMonth(int year, int month)
        {
            return year >= DateTime.MinValue.Year && 
                year <= DateTime.MaxValue.Year && 
                month >= 1 && month <= 12;
        }

    }

    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message) : base(message) {}
    }

}
