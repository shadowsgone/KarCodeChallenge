using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.GetUser
{
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, User>
    {
        private readonly ILogger<GetUserCommandHandler> _logger;
        private readonly IUsersContext _context;

        public GetUserCommandHandler(ILogger<GetUserCommandHandler> logger, IUsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id);
        }
    }
}
