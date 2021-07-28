using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.UpdateBank
{
    public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, bool>
    {
        private readonly ILogger<UpdateBankCommandHandler> _logger;
        private readonly IBankContext _context;

        public UpdateBankCommandHandler(ILogger<UpdateBankCommandHandler> logger, IBankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
        {
            var bank = await _context.Banks.SingleOrDefaultAsync(b => b.Id == request.Id);
            if (bank == null)
            {
                return false;
            }

            bank.Name = request.Bank.Name;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
