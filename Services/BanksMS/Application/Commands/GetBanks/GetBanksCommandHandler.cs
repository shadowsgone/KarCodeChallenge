using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetBanks
{
    public class GetBanksCommandHandler : IRequestHandler<GetBanksCommand, GetBanksResponse>
    {
        private readonly ILogger<GetBanksCommandHandler> _logger;
        private readonly IBankContext _context;

        public GetBanksCommandHandler(ILogger<GetBanksCommandHandler> logger, IBankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<GetBanksResponse> Handle(GetBanksCommand request, CancellationToken cancellationToken)
        {
            //TODO: add paging / filtering
            var banks = await _context.Banks.ToListAsync();
            return new GetBanksResponse
            {
                Banks = banks,
            };
        }
    }
}
