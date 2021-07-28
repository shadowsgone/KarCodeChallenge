using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.DeleteBank
{
    public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand, Bank>
    {
        private readonly ILogger<DeleteBankCommandHandler> _logger;
        private readonly IBankContext _context;

        public DeleteBankCommandHandler(ILogger<DeleteBankCommandHandler> logger, IBankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Bank> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
        {
            var bank = await _context.Banks.SingleOrDefaultAsync(b => b.Id == request.Id);
            if (bank != null)
            {
                _context.Banks.Remove(bank);
                await _context.SaveChangesAsync();
            }

            return bank;
        }
    }
}
