using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, BankAccount>
    {
        private readonly ILogger<DeleteAccountCommandHandler> _logger;
        private readonly IBankContext _context;

        public DeleteAccountCommandHandler(ILogger<DeleteAccountCommandHandler> logger, IBankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<BankAccount> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _context.BankAccounts.SingleOrDefaultAsync(ba => ba.BankId == request.BankId && 
                ba.AccountId == request.AccountId);
            if (bankAccount != null)
            {
                _context.BankAccounts.Remove(bankAccount);
                await _context.SaveChangesAsync();
            }

            return bankAccount;
        }
    }
}
