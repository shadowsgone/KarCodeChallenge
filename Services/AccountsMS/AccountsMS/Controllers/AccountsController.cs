using Application.Commands.CreateAccount;
using Application.Commands.CreateAccountUser;
using Application.Commands.DeleteAccount;
using Application.Commands.DeleteAccoutUser;
using Application.Commands.GetAccount;
using Application.Commands.GetAccountUsers;
using Application.Commands.GetBalance;
using Application.Commands.UpdateAccount;
using Application.Commands.UpdateBalance;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AccountsMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IMediator _mediator;

        public AccountsController(ILogger<AccountsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAccount([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new GetAccountCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to get Account({id}).");
                return BadRequest($"Failed to get Account({id}).");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAccount([FromBody] Account Account)
        {
            try
            {
                var response = await _mediator.Send(new CreateAccountCommand(Account));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to create Account.");
                return BadRequest("Failed to create Account.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAccount([FromRoute] int id, [FromBody] Account account)
        {
            try
            {
                var response = await _mediator.Send(new UpdateAccountCommand(id, account));
                if (response)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to update Account({id}).");
            }

            return BadRequest($"Failed to update Account({id}).");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteAccountCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to delete Account({id}).");
                return BadRequest($"Failed to delete Account({id}).");
            }
        }

        [HttpGet("{id}/users")]
        [ProducesResponseType(typeof(GetAccountUsersResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAccountUsers([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new GetAccountUsersCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to get Account({id}) Users.");
                return BadRequest($"Failed to get Account({id}) Users.");
            }
        }

        [HttpPost("{id}/users")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAccountUser([FromRoute]int id, [FromBody] AccountUser accountUser)
        {
            try
            {
                var response = await _mediator.Send(new CreateAccountUserCommand(id, accountUser));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to create Account User.");
                return BadRequest("Failed to create Account User.");
            }
        }

        [HttpDelete("{id}/users/{userId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAccountUser([FromRoute]int id, [FromRoute]int userId)
        {
            try
            {
                var response = await _mediator.Send(new DeleteAccountUserCommand(id, userId));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to delete Account User.");
                return BadRequest("Failed to delete Account User.");
            }
        }

        [HttpGet("{id}/balance")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBalance([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new GetBalanceCommand(id));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to get Account({id}).");
                return BadRequest($"Failed to get Account({id}).");
            }
        }

        [HttpPut("{id}/balance")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateBalance([FromRoute] int id, [FromBody]Transaction transaction)
        {
            try
            {
                var response = await _mediator.Send(new UpdateBalanceCommand(id, transaction));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to get Account({id}).");
                return BadRequest($"Failed to get Account({id}).");
            }
        }
    }
}

