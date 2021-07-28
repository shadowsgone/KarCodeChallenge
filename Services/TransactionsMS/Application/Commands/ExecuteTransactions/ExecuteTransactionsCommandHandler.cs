using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.ExecuteTransactions
{
    public class ExecuteTransactionsCommandHandler : IRequestHandler<ExecuteTransactionsCommand, bool>
    {
        private readonly ILogger<ExecuteTransactionsCommandHandler> _logger;
        private readonly ITransactionContext _context;
        private readonly IAccountClient _accountClient;

        public ExecuteTransactionsCommandHandler(ILogger<ExecuteTransactionsCommandHandler> logger, 
            ITransactionContext context, 
            IAccountClient accountClient)
        {
            _logger = logger;
            _context = context;
            _accountClient = accountClient;
        }

        public async Task<bool> Handle(ExecuteTransactionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transactions = await _context.Transactions.Where(t => t.ExecutionDate <= DateTimeOffset.UtcNow &&
                        t.HasExecuted == false &&
                        t.IsActive == true)
                    .ToListAsync();

                List<Task> tasks = new List<Task>();
                foreach(var transaction in transactions)
                {
                    transaction.HasExecuted = true;
                    tasks.Add(_accountClient.UpdateAccountBalanceAsync(transaction));
                }

                //TODO: handle if one of the tasks fails
                Task.WaitAll(tasks.ToArray());

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed executing transactions.");
            }

            return false;
        }
    }
}
