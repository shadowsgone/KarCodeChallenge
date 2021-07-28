using Application.Commands.AddAccount;
using Application.Commands.CreateBank;
using Application.Commands.DeleteAccount;
using Application.Commands.DeleteBank;
using Application.Commands.GetBank;
using Application.Commands.GetBankAccounts;
using Application.Commands.GetBanks;
using Application.Commands.UpdateBank;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BanksMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BanksController : ControllerBase
    {
        private readonly ILogger<BanksController> _logger;
        private readonly IMediator _mediator;

        public BanksController(ILogger<BanksController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetBanksResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBanks()
        {
            try
            {
                var response = await _mediator.Send(new GetBanksCommand());
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to get Banks.");
                return BadRequest("Failed to get Banks.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Bank), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetBank([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new GetBankCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to get Bank({id}).");
                return BadRequest($"Failed to get Bank({id}).");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateBank([FromBody] Bank Bank)
        {
            try
            {
                var response = await _mediator.Send(new CreateBankCommand(Bank));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to create Bank.");
                return BadRequest("Failed to create Bank.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateBank([FromRoute] int id, [FromBody] Bank Bank)
        {
            try
            {
                var response = await _mediator.Send(new UpdateBankCommand(id, Bank));
                if (response)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to update Bank({id}).");
            }

            return BadRequest($"Failed to update Bank({id}).");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Bank), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteBank([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteBankCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to delete Bank({id}).");
                return BadRequest($"Failed to delete Bank({id}).");
            }
        }

        [HttpGet("{id}/accounts")]
        [ProducesResponseType(typeof(GetBankAccountsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetBankAccounts([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new GetBankAccountsCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to get Bank({id}) Accounts.");
                return BadRequest($"Failed to get Bank({id}) Accounts.");
            }
        }

        [HttpPost("{id}/accounts")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddAccount([FromRoute] int id, [FromBody] AddAccountCommand command)
        {
            try
            {
                command.BankId = id;
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to add Bank({id}) Account.");
                return BadRequest($"Failed to add Bank({id}) Account.");
            }
        }

        [HttpDelete("{id}/accounts/{accountId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id, [FromRoute] int accountId)
        {
            try
            {
                var response = await _mediator.Send(new DeleteAccountCommand(id, accountId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to delete Bank({id}) Account({accountId}).");
                return BadRequest($"Failed to delete Bank({id}) Account({accountId}).");
            }
        }
    }
}
