using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Utils;
using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.Factories.Abstract;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

namespace FinancasPessoais.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialReleaseController : ControllerBase
    {
        #region Injected Services

        private readonly IAccountService _accountService;
        private readonly ICreditCardService _creditCardService;
        private readonly IFinancialReleaseFactory _financialReleaseFactory;
        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor

        public FinancialReleaseController(IAccountService accountService,
            ICreditCardService creditCardService,
            IFinancialReleaseFactory financialReleaseFactory, 
            ICategoryService categoryService)
        {
            _accountService = accountService;
            _creditCardService = creditCardService;
            _financialReleaseFactory = financialReleaseFactory;
            _categoryService = categoryService;
        }

        #endregion

        #region Endpoints

        [HttpGet()]
        [Route("getFinancialReleaseById/{id:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFinancialReleaseById(Guid id)
        {
            try
            {
                var financialRelease = await _financialReleaseFactory.GetFinancialReleaseByIdAsync(id);
                if (financialRelease == null)
                    return NotFound(Constants.EntityNotFound("Financial Release"));

                return Ok(financialRelease);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost]
        [Route("createFinancialRelease")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFinancialRelease([FromBody] FinancialReleaseRequestDTO request)
        {
            await PreValidations(request);

            try
            {
                var userID = LoggedUserId();
                var financialReleaseResponseDTO = await _financialReleaseFactory.CreateFinancialReleaseAsync(request, userID);

                return Created(string.Empty, financialReleaseResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("createPurchaseInInstallments")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePurchaseInInstallments([FromBody] PurchaseInInstallmentsRequestDTO request)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(request.CategoryId);
                if (category == null)
                    return BadRequest(Constants.EntityNotExistError(Operations.Insert, "purcharse in installments", "category"));

                var purchase = await _financialReleaseFactory.CreateInstallmentPurchaseOnCreditCard(request);

                return Created(string.Empty, purchase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPut]
        [Route("payCreditCardRelease")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PayCreditCardRelease([FromBody] CreditCardReleasePaymentRequestDTO request)
        {
            try
            {
                var creditCardRelease = await _financialReleaseFactory.GetFinancialReleaseByIdAsync(request.Id);
                if (creditCardRelease == null)
                    return NotFound("Cartão de crédito não encontrado.");

                var account = await _accountService.GetAccountModelByIdAsync(request.AccountId);
                if (account == null)
                    return NotFound("Conta não encontrada.");

                if (request.PaymentDate < creditCardRelease.ReleaseDate)
                    return BadRequest(Constants.PAYMENT_DATE_INVALID);

                await _financialReleaseFactory.PayCreditCardRelease(request);

                return Ok(Constants.PAYMENT_REGISTERED_SUCCESSFULY);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost]
        [Route("getExtractByAccountId")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetExtractByAccountId([FromBody] ExtractRequestDTO request)
        {
            try
            {
                var userID = LoggedUserId();

                var extract = await _accountService.GetExtractByAccountId(request, userID);
                if (extract == null)
                    return NotFound(Constants.EXTRACT_NOT_FOUND);

                return Ok(extract);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost]
        [Route("getMonthlyExtractByAccountId")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMonthlyExtractByAccountId([FromBody] MonthlyExtractRequestDTO request)
        {
            try
            {
                var userID = LoggedUserId();

                var extract = await _accountService.GetMonthlyExtractByAccountId(request, userID);
                return Ok(extract);
            }
            catch (InvalidRequestException ex)
            {
                return BadRequest($"Erro na requisição: {ex.Message}"); // Retorna mensagem de erro amigável para argumentos inválidos
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message); // Retorna uma mensagem de erro genérica para outras exceções
            }
        }

        [HttpDelete]
        [Route("deleteFinancialRelease/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFinancialRelease(Guid id)
        {
            try
            {
                var userID = LoggedUserId();
                
                await _financialReleaseFactory.DeleteFinancialReleaseAsync(id, userID);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(Constants.EntityNotFound("Financial Release"));
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPost]
        [Route("getInvoiceByCreditCardId/{creditCardId:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetInvoiceByCreditCardId(Guid creditCardId) 
        {
            var creditCard = await _creditCardService.GetCreditCardAsync(creditCardId);
            if (creditCard == null)
                return BadRequest("Cartão de crédito não encontrado.");

            var releases = await _financialReleaseFactory.GetInvoiceByCreditCardId(creditCard);
            
            return Ok(releases);
        }

        [HttpPost]
        [Route("chart")]
        public async Task<IActionResult> Chart([FromBody] ChartRequest request) 
        {
            var financialReleases = await _financialReleaseFactory.GetExpensesGroupedByCategoryAsync(request);

            return Ok(financialReleases);
        }

        #endregion

        #region Private Methods

        private async Task PreValidations(FinancialReleaseRequestDTO request)
        {
            if (ReleaseDateIsGreatherThanCurrentDate(request.ReleaseDate))
                throw new Exception(Constants.FAILED_INCLUDE_FINANCIALRELEASE_DATE_GREATER_THAN_CURRENT_DATE);
            
            if (await CategoryDoesNotExist(request))
                throw new Exception(Constants.EntityNotExistError(Operations.Insert, "financial release", "category"));

            if (request.Type == ReleaseTypes.Income && request.CreditCardId != null)
                throw new Exception("Não é possível incluir lançamento do tipo Receita em Cartão de Crédito.");
        }

        private static bool ReleaseDateIsGreatherThanCurrentDate(DateTime releaseDate)
        {
            return releaseDate.Date > DateTime.Today;
        }

        private async Task<bool> CategoryDoesNotExist(FinancialReleaseRequestDTO request)
        {
            return await _categoryService.GetByIdAsync(request.CategoryId) == null;
        }

        private string LoggedUserId() 
        {
            return User.FindFirst(ClaimTypes.PrimarySid)?.Value;
        } 

        #endregion

    }

}
