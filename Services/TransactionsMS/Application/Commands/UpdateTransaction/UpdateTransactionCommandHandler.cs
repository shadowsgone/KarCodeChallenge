using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, bool>
    {
        private readonly ILogger<UpdateTransactionCommandHandler> _logger;
        private readonly ITransactionContext _context;
        private readonly IAccountClient _accountClient;

        public UpdateTransactionCommandHandler(ILogger<UpdateTransactionCommandHandler> logger,
            ITransactionContext context,
            IAccountClient accountClient)
        {
            _logger = logger;
            _context = context;
            _accountClient = accountClient;
        }

        public async Task<bool> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(t => t.Id == request.Id);
            if(transaction == null)
            {
                return false;
            }

            transaction.ModifiedBy = request.Transaction.ModifiedBy;
            transaction.ModifiedDate = DateTimeOffset.UtcNow;
            transaction.Amount = request.Transaction.Amount;
            transaction.ExecutionDate = request.Transaction.ExecutionDate;
            transaction.IsActive = request.Transaction.IsActive;

            if (transaction.IsActive && (!transaction.ExecutionDate.HasValue || transaction.ExecutionDate.Value <= DateTimeOffset.UtcNow))
            {
                request.Transaction.HasExecuted = await _accountClient.UpdateAccountBalanceAsync(request.Transaction);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
