using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetBank
{
    public class GetBankCommandHandler : IRequestHandler<GetBankCommand, Bank>
    {
        private readonly ILogger<GetBankCommandHandler> _logger;
        private readonly IBankContext _context;

        public GetBankCommandHandler(ILogger<GetBankCommandHandler> logger, IBankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<Bank> Handle(GetBankCommand request, CancellationToken cancellationToken)
        {
            return _context.Banks.SingleOrDefaultAsync(b => b.Id == request.Id);
        }
    }
}
