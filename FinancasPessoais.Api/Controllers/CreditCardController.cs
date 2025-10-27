using FinancasPessoais.Application.DTOs.Requests;
using FinancasPessoais.Application.Interfaces;
using FinancasPessoais.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinancasPessoais.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : ControllerBase
    {
        #region Injected Services

        private readonly ICreditCardService _creditCardService;
        
        #endregion

        #region Constructor

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }
        
        #endregion

        #region Endpoints

        [HttpGet]
        [Route("getCreditCards")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCreditCards()
        {
            try
            {
                var creditCards = await _creditCardService.GetAsync();
                foreach (var creditCard in creditCards) 
                {
                    DateTime closingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, creditCard.InvoiceClosingDate);

                    var creditCardReleases = await _creditCardService.GetCreditCardReleasesByCreditCardId(creditCard.Id, closingDate);

                    creditCard.CurrentInvoice = creditCardReleases.Sum(c => c.Value);

                    //TODO: Considerar valor das compras parceladas ao calcular o saldo do cartão
                    creditCard.Balance = creditCard.CardLimit - creditCard.CurrentInvoice;
                };

                return Ok(creditCards);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpGet]
        [Route("getCreditCardById/{id:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCreditCardById(Guid id)
        {
            try
            {
                var creditCard = await _creditCardService.GetCreditCardByIdAsync(id);
                if (creditCard == null)
                    return NotFound(Constants.EntityNotFound("Credit card"));

                DateTime closingDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, creditCard.InvoiceClosingDate);
                DateTime lastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, creditCard.InvoiceClosingDate);

                var creditCardReleases = await _creditCardService.GetCreditCardReleasesByCreditCardId(creditCard.Id, closingDate);

                var totalReleases = creditCardReleases
                    .Sum(c => c.Value);

                creditCard.Balance = creditCard.CardLimit - totalReleases;

                creditCard.CurrentInvoice = creditCardReleases
                    .Where(r => r.ReleaseDate < lastDay)
                    .Sum(c => c.Value);
                
                return Ok(creditCard);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        [Route("createCreditCard")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCreditCard([FromBody] CreditCardRequestDTO creditCardRequestDTO)
        {
            try
            {
                var creditCard = await _creditCardService.GetCreditCardModelByNumberAsync(creditCardRequestDTO.CardNumber);
                if (creditCard != null)
                    return BadRequest(Constants.CREDIT_CARD_ALREADY_EXISTS);

                await _creditCardService.CreateCreditCardAsync(creditCardRequestDTO);

                return Created(string.Empty, "Cartão de Crédito criado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpDelete()]
        [Route("removeCreditCard/{id:Guid}")]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCreditCard(Guid id) 
        {
            try
            {
                var creditCard = await _creditCardService.GetCreditCardAsync(id);
                if (creditCard == null)
                    return NotFound(Constants.EntityNotFound("Credit card"));

                if (creditCard.FinancialReleases != null && creditCard.FinancialReleases.Any())
                    return BadRequest(Constants.CREDIT_CARD_WITH_FINANCIAL_RELEASES);

                await _creditCardService.RemoveCreditCardAsync(creditCard);

                return Ok();
            }

            
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        #endregion

        #region Private Methods
        #endregion

    }
}
