using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetTransactions
{
    public class GetTransactionsCommandHandler : IRequestHandler<GetTransactionsCommand, GetTransactionsResponse>
    {
        private readonly ILogger<GetTransactionsCommandHandler> _logger;
        private readonly ITransactionContext _context;

        public GetTransactionsCommandHandler(ILogger<GetTransactionsCommandHandler> logger, ITransactionContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<GetTransactionsResponse> Handle(GetTransactionsCommand request, CancellationToken cancellationToken)
        {
            var transactions = await _context.Transactions.Where(t => t.IsActive == request.IsActive && 
                (request.AccountIds.Contains(t.AccountId) || 
                request.UserIds.Contains(t.UserId)))
                .ToListAsync();

            return new GetTransactionsResponse
            {
                Transactions = transactions
            };
        }
    }
}
