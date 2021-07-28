using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.UpdateBalance
{
    public class UpdateBalanceCommandHandler : IRequestHandler<UpdateBalanceCommand, decimal>
    {
        private readonly ILogger<UpdateBalanceCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public UpdateBalanceCommandHandler(ILogger<UpdateBalanceCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<decimal> Handle(UpdateBalanceCommand request, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Id == request.Id);
            if (account == null)
            {
                throw new Exception("Failed to find account");
            }

            var amount = request.Transaction.Amount;
            if(amount > 0 && (request.Transaction.Type == TransactionTypes.Transfer || 
                request.Transaction.Type == TransactionTypes.Withdraw))
            {
                amount *= -1;
            }

            //TODO: transfers might not be considered withdraws?
            if (account.Type == AccountTypes.InvestmentIndividual && amount < -500)
            {
                amount = -500;
            }

            account.Balance += amount;

            await _context.SaveChangesAsync();

            return account.Balance;
        }
    }
}
