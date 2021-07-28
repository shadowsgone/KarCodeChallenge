using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.CreateAccountUser
{
    public class CreateAccountUserCommandHandler : IRequestHandler<CreateAccountUserCommand, int>
    {
        private readonly ILogger<CreateAccountUserCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public CreateAccountUserCommandHandler(ILogger<CreateAccountUserCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<int> Handle(CreateAccountUserCommand request, CancellationToken cancellationToken)
        {
            request.AccountUser.AccountId = request.Id;

            //TODO: We should validate this user, consider using AutoMapper 
            await _context.AccountUsers.AddAsync(request.AccountUser);
            await _context.SaveChangesAsync();

            return request.AccountUser.Id;
        }
    }
}
