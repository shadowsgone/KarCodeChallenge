using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly ILogger<CreateTransactionCommandHandler> _logger;
        private readonly ITransactionContext _context;
        private readonly IAccountClient _accountClient;

        public CreateTransactionCommandHandler(ILogger<CreateTransactionCommandHandler> logger,
            ITransactionContext context,
            IAccountClient accountClient)
        {
            _logger = logger;
            _context = context;
            _accountClient = accountClient;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if(!request.Transaction.ExecutionDate.HasValue || request.Transaction.ExecutionDate.Value <= DateTimeOffset.UtcNow)
            {
                request.Transaction.HasExecuted = await _accountClient.UpdateAccountBalanceAsync(request.Transaction);
            }

            request.Transaction.CreatedDate = DateTimeOffset.UtcNow;
            await _context.Transactions.AddAsync(request.Transaction);
            await _context.SaveChangesAsync();

            return request.Transaction.Id;
        }
    }
}
