using Application.Commands.CreateTransaction;
using Application.Commands.ExecuteTransactions;
using Application.Commands.GetTransaction;
using Application.Commands.GetTransactions;
using Application.Commands.UpdateTransaction;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TransactionsMS.Controllers
{
    //TODO: Use CancellationTokens
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IMediator _mediator;

        public TransactionsController(ILogger<TransactionsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetTransactionsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTransactions([FromBody] GetTransactionsCommand command)
        {
            try
            {
                //TODO: we should validate the command coming in.
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to get Transactions.");
                return BadRequest("Failed to get Transactions.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transaction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetTransaction([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new GetTransactionCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to get Transaction({id}).");
                return BadRequest($"Failed to get Transaction({id}).");
            }
        }

        [HttpGet("execute")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ExecuteTransactions()
        {
            try
            {
                var response = await _mediator.Send(new ExecuteTransactionsCommand());
                if (response)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to execute Transactions.");
            }

            return BadRequest("Failed to execute Transactions.");
        }

        [HttpGet("{id}/execute")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ExecuteTransaction([FromRoute]int id)
        {
            try
            {
                var response = await _mediator.Send(new ExecuteTransactionCommand(id));
                if (response)
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to execute Transactions({id})");
            }

            return BadRequest($"Failed to execute Transactions({id})");
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction Transaction)
        {
            try
            {
                var response = await _mediator.Send(new CreateTransactionCommand(Transaction));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to create Transaction.");
                return BadRequest("Failed to create Transaction.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateTransaction([FromRoute] int id, [FromBody] Transaction Transaction)
        {
            try
            {
                var response = await _mediator.Send(new UpdateTransactionCommand(id, Transaction));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to update Transaction.");
                return BadRequest("Failed to update Transaction.");
            }
        }
    }
}
