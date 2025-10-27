using AutoMapper;
using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.DTOs.Responses;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasPessoais.Application.Services
{
    public class AccountPayableService : IAccountPayableService
    {
        private IAccountPayableRepository _accountPayableRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountPayableService> _logger;
        private readonly IMapper _mapper;

        public AccountPayableService(IAccountPayableRepository accountPayableRepository, IMapper mapper, IEmailService emailService, ILogger<AccountPayableService> logger)
        {
            _accountPayableRepository = accountPayableRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<IEnumerable<AccountPayableResponseDTO>> GetAccountsPayableAsync()
        {
            var accountsPayable = await _accountPayableRepository.GetAsync();
            return _mapper.Map<List<AccountPayableResponseDTO>>(accountsPayable);
        }

        public async Task<AccountPayableResponseDTO> GetAccountPayableByIdAsync(Guid id)
        {
            var accountPayable = await _accountPayableRepository.GetByIdAsync(id);
            return _mapper.Map<AccountPayableResponseDTO>(accountPayable);
        }

        public async Task CreateAccountPayableAsync(AccountPayableRequestDTO request, string filePatch) 
        {
            var accountPayable = new AccountPayable(
                    request.DueDate,
                    request.Value,
                    request.SubcategoryId,
                    request.Description,
                    request.BarCode,
                    request.ScheduleDate,
                    request.Emails,
                    filePatch);
            
            await _accountPayableRepository.CreateAsync(accountPayable);
        }

        public async Task CheckAndSendReminders()
        {
            var today = DateTime.Today;
            var accountsPayable = await _accountPayableRepository.GetScheduledAccountsPayable(today);

            if (accountsPayable != null && accountsPayable.Any()) 
            {
                foreach (var accountPayable in accountsPayable) 
                {
                    _emailService.SendAccountPayableEmail(accountPayable);
                    var message = $"Simulando envio de e-mail para {accountPayable.Emails}";
                    _logger.LogInformation(message);
                }
            }
        }

    }
}
