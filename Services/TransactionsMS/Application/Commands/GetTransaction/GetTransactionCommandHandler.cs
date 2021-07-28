using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetTransaction
{
    public class GetTransactionCommandHandler : IRequestHandler<GetTransactionCommand, Transaction>
    {
        private readonly ILogger<GetTransactionCommandHandler> _logger;
        private readonly ITransactionContext _context;

        public GetTransactionCommandHandler(ILogger<GetTransactionCommandHandler> logger, ITransactionContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<Transaction> Handle(GetTransactionCommand request, CancellationToken cancellationToken)
        {
            return _context.Transactions.SingleOrDefaultAsync(t => t.Id == request.Id);
        }
    }
}
