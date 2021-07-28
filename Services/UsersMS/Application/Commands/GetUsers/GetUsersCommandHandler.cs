using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetUsers
{
    public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, GetUsersResponse>
    {
        private readonly ILogger<GetUsersCommandHandler> _logger;
        private readonly IUsersContext _context;

        public GetUsersCommandHandler(ILogger<GetUsersCommandHandler> logger, IUsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<GetUsersResponse> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync();
            return new GetUsersResponse
            {
                Users = users
            };
        }
    }
}
