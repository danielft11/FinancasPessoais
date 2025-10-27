using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.DTOs.Requests
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
