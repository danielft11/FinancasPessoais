using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class AccountPayableRequestDTO
    {
        [Required(ErrorMessage = "The field DueDate is required")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "The field Value is required")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "The field SubcategoryId is required")]
        public Guid SubcategoryId { get; set; }

        [Required(ErrorMessage = "The field Description is required")]
        public string Description { get; set; }

        public string BarCode { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string Emails { get; set; }
       
    }

}
