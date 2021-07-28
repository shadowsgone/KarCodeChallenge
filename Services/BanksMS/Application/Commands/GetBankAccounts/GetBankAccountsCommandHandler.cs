using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetBankAccounts
{
    public class GetBankAccountsCommandHandler : IRequestHandler<GetBankAccountsCommand, GetBankAccountsResponse>
    {
        private readonly ILogger<GetBankAccountsCommandHandler> _logger;
        private readonly IBankContext _context;

        public GetBankAccountsCommandHandler(ILogger<GetBankAccountsCommandHandler> logger, IBankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<GetBankAccountsResponse> Handle(GetBankAccountsCommand request, CancellationToken cancellationToken)
        {
            var bankAccounts = await _context.BankAccounts.Where(ba => ba.BankId == request.Id)
                .ToListAsync();

            return new GetBankAccountsResponse
            {
                BankAccounts = bankAccounts
            };
        }
    }
}
