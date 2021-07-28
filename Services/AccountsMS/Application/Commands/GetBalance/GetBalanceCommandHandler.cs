using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetBalance
{
    public class GetBalanceCommandHandler : IRequestHandler<GetBalanceCommand, decimal>
    {
        private readonly ILogger<GetBalanceCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public GetBalanceCommandHandler(ILogger<GetBalanceCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<decimal> Handle(GetBalanceCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == request.Id);
            if(account == null)
            {
                //TODO: We should create our own exception handling.
                throw new Exception("Account could not be found.");
            }

            return account.Balance;
        }
    }
}
