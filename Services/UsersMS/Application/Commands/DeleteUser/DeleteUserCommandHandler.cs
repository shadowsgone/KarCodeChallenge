using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, User>
    {
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IUsersContext _context;

        public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<User> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }
    }
}
