using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetAccount
{
    public class GetAccountCommandHandler : IRequestHandler<GetAccountCommand, Account>
    {
        private readonly ILogger<GetAccountCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public GetAccountCommandHandler(ILogger<GetAccountCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<Account> Handle(GetAccountCommand request, CancellationToken cancellationToken)
        {
            return _context.Accounts.SingleOrDefaultAsync(a => a.Id == request.Id);
        }
    }
}
