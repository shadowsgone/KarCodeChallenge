using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Account>
    {
        private readonly ILogger<DeleteAccountCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public DeleteAccountCommandHandler(ILogger<DeleteAccountCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Account> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == request.Id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }

            return account;
        }
    }
}
