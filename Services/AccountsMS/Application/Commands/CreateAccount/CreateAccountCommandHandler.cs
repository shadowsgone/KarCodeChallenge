using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly ILogger<CreateAccountCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public CreateAccountCommandHandler(ILogger<CreateAccountCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            //TODO: We should validate this account, consider using AutoMapper 
            request.Account.CreatedDate = DateTimeOffset.UtcNow;
            await _context.Accounts.AddAsync(request.Account);
            await _context.SaveChangesAsync();

            return request.Account.Id;
        }
    }
}
