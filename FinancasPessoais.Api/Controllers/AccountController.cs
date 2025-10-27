using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinancasPessoais.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        #region Injected Services
        private readonly IAccountService _accountService;
        private readonly ISubcategoryService _subcategoryService;
        #endregion

        #region Constructor
        public AccountController(IAccountService accountService, ISubcategoryService subcategoryService)
        {
            _accountService = accountService;
            _subcategoryService = subcategoryService;
        }
        #endregion

        #region Endpoins

        [HttpGet]
        [Route("getAccounts")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAccountAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("getDetailedAccounts")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDetailedAccounts()
        {
            try
            {
                var accounts = await _accountService.GetDetailedAccountsAsync();
                if (accounts == null)
                    return NotFound(Constants.EntityNotFound("Account"));

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpGet()]
        [Route("getAccountById/{id:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            try
            {
                var account = await _accountService.GetAccountByIdAsync(id);
                if (account == null)
                    return NotFound(Constants.EntityNotFound("Account"));

                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost]
        [Route("createAccount")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequestDTO accountDTO)
        {
            try
            {
                accountDTO.AccountNumber = RemoveInvalidCharacters(accountDTO.AccountNumber);

                if (!string.IsNullOrEmpty(accountDTO.Name) && !string.IsNullOrEmpty(accountDTO.AccountNumber))
                {
                    var account = await _accountService.GetAccountByNameAndNumberAsync(accountDTO.Name, accountDTO.AccountNumber);

                    if (account != null)
                        return BadRequest(Constants.ACCOUNT_ALREADY_EXISTS);
                }

                var accountToInsert = await _accountService.CreateAccountAsync(accountDTO);

                return Created(string.Empty, accountToInsert);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPut]
        [Route("updateAccount")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAccount([FromQuery] Guid Id, [FromBody] AccountRequestDTO accountDTO)
        {
            try
            {
                var account = await _accountService.GetAccountModelByIdAsync(Id);

                if (account == null)
                    return NotFound(Constants.EntityNotExistError(Operations.Update, "account", "account"));

                account.UpdateModel(accountDTO.Name, accountDTO.BankBranch, accountDTO.AccountNumber);

                return Ok(await _accountService.UpdateAccountAsync(account));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete()]
        [Route("removeAccount/{id:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAccount(Guid id)
        {
            try
            {
                var account = await _accountService.GetAccoundModelByIdWithFinancialReleases(id);

                if (account == null)
                    return NotFound(Constants.EntityNotExistError(Operations.Remove, "account", "account"));

                if (account.FinancialReleases.Any())
                    return BadRequest(Constants.ACCOUNT_WITH_FINANCIAL_RELEASES);

                await _accountService.RemoveAccountAsync(account);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        #endregion

        #region Private methods

        private static string RemoveInvalidCharacters(string field)
        {
            if (!string.IsNullOrEmpty(field) && field.Contains("-"))
                return field.Replace("-", string.Empty);

            return field;

        }

        #endregion

    }
}
