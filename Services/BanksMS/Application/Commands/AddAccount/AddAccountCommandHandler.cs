using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.AddAccount
{
    public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, int>
    {
        private readonly ILogger<AddAccountCommandHandler> _logger;
        private readonly IBankContext _context;

        public AddAccountCommandHandler(ILogger<AddAccountCommandHandler> logger, IBankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<int> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = new BankAccount
            {
                AccountId = request.AccountId,
                BankId = request.BankId,
            };

            //TODO: We should validate that this user, consider using AutoMapper 
            await _context.BankAccounts.AddAsync(bankAccount);
            await _context.SaveChangesAsync();

            return bankAccount.Id;
        }
    }
}
