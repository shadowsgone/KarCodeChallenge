using Application.Commands.CreateUser;
using Application.Commands.DeleteUser;
using Application.Commands.GetUser;
using Application.Commands.GetUsers;
using Application.Commands.UpdateUser;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace UsersMS.Controllers
{
    //TODO: Use CancellationTokens
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetUsersResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var response = await _mediator.Send(new GetUsersCommand());
                return Ok(response);
            }
            catch(Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to get users.");
                return BadRequest("Failed to get users.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new GetUserCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to get user({id}).");
                return BadRequest($"Failed to get user({id}).");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody]User user)
        {
            try
            {
                var response = await _mediator.Send(new CreateUserCommand(user));
                return Ok(response);
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, "Failed to create user.");
                return BadRequest("Failed to create user.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody]User user)
        {
            try
            {
                var response = await _mediator.Send(new UpdateUserCommand(id, user));
                if(response)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to update user({id}).");
            }

            return BadRequest($"Failed to update user({id}).");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteUserCommand(id));
                if (response != null)
                {
                    return Ok(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: handle errors more gracefully.
                _logger.LogError(ex, $"Failed to delete user({id}).");
                return BadRequest($"Failed to delete user({id}).");
            }
        }
    }
}
