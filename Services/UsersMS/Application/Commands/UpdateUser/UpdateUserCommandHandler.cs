using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IUsersContext _context;

        public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IUsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id);
            if(user == null)
            {
                return false;
            }

            user.FirstName = request.User.FirstName;
            user.LastName = request.User.LastName;
            user.IsActive = request.User.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
