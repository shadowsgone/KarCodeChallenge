using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IUsersContext _context;

        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //TODO: We should validate this user, consider using AutoMapper 
            request.User.CreatedDate = DateTimeOffset.UtcNow;
            await _context.Users.AddAsync(request.User);
            await _context.SaveChangesAsync();

            return request.User.Id;
        }
    }
}
