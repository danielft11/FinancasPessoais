using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Infra.IoC;
using FinancasPessoais.Application.DTOs.Requests;

namespace FinancasPessoais.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountPayableController : ControllerBase
    {
        #region Injected Services

        private readonly IAccountPayableService _accountPayableService;
        private readonly IFileService _fileService;

        #endregion

        #region Constructor 

        public AccountPayableController(IAccountPayableService accountPayableService, IFileService fileService)
        {
            _accountPayableService = accountPayableService;
            _fileService = fileService;
        }

        #endregion

        [HttpGet]
        [Route("getAccountsPayable")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountsPayable()
        {
            try
            {
                var accountPayable = await _accountPayableService.GetAccountsPayableAsync();
                return Ok(accountPayable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("getAccountPayableById/{id:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountPayableById(Guid id)
        {
            try
            {
                var accountPayable = await _accountPayableService.GetAccountPayableByIdAsync(id);
                
                return Ok(accountPayable);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Route("createAccountPayable")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccountPayable([FromForm] AccountPayableRequestDTO request, [FromForm] IFormFile file) 
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo foi enviado.");

            try
            {
                var filePath = _fileService.SaveFile(request.Description, file, request.DueDate);

                await _accountPayableService.CreateAccountPayableAsync(request, filePath);

                return Ok("Conta cadastrada com sucesso.");
            }
            catch (Exception ex) 
            {
                return BadRequest($"Erro ao salvar a conta a pagar: {ex.InnerException.Message}");
            }
            
        }

    }
}
