using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Interfaces
{
    public interface IAccountPayableService
    {
        Task<IEnumerable<AccountPayableResponseDTO>> GetAccountsPayableAsync();
        Task<AccountPayableResponseDTO> GetAccountPayableByIdAsync(Guid id);
        Task CreateAccountPayableAsync(AccountPayableRequestDTO request, string filePatch);
    }
}
