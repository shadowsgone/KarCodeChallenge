using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.UpdateAccount
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, bool>
    {
        private readonly ILogger<UpdateAccountCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public UpdateAccountCommandHandler(ILogger<UpdateAccountCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == request.Id);
            if (account == null)
            {
                return false;
            }

            account.Balance = request.Account.Balance;
            account.BankId = request.Account.BankId;
            account.Name = request.Account.Name;
            account.OwnerId = request.Account.OwnerId;
            account.Type = request.Account.Type;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
