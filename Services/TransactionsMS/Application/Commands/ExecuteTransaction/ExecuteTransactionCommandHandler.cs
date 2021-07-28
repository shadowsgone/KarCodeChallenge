using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.ExecuteTransactions
{
    public class ExecuteTransactionCommandHandler : IRequestHandler<ExecuteTransactionCommand, bool>
    {
        private readonly ILogger<ExecuteTransactionCommandHandler> _logger;
        private readonly ITransactionContext _context;
        private readonly IAccountClient _accountClient;

        public ExecuteTransactionCommandHandler(ILogger<ExecuteTransactionCommandHandler> logger, 
            ITransactionContext context, 
            IAccountClient accountClient)
        {
            _logger = logger;
            _context = context;
            _accountClient = accountClient;
        }

        public async Task<bool> Handle(ExecuteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(t => t.Id == request.Id && 
                        t.ExecutionDate <= DateTimeOffset.UtcNow &&
                        t.HasExecuted == false &&
                        t.IsActive == true);

            if (transaction == null)
            {
                return false;
            }

            var response = await _accountClient.UpdateAccountBalanceAsync(transaction);

            transaction.HasExecuted = response;

            await _context.SaveChangesAsync();

            return response;
        }
    }
}
