using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetAccountUsers
{
    public class GetAccountUsersCommandHandler : IRequestHandler<GetAccountUsersCommand, GetAccountUsersResponse>
    {
        private readonly ILogger<GetAccountUsersCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public GetAccountUsersCommandHandler(ILogger<GetAccountUsersCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<GetAccountUsersResponse> Handle(GetAccountUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _context.AccountUsers.Where(au => au.AccountId == request.Id)
                .ToListAsync();
            return new GetAccountUsersResponse
            {
                AccountUsers = users
            };
        }
    }
}
