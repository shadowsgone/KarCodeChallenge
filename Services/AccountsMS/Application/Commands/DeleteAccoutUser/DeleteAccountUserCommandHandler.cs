using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.DeleteAccoutUser
{
    public class DeleteAccountUserCommandHandler : IRequestHandler<DeleteAccountUserCommand, AccountUser>
    {
        private readonly ILogger<DeleteAccountUserCommandHandler> _logger;
        private readonly IAccountsContext _context;

        public DeleteAccountUserCommandHandler(ILogger<DeleteAccountUserCommandHandler> logger, IAccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<AccountUser> Handle(DeleteAccountUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.AccountUsers.SingleOrDefaultAsync(u => u.AccountId == request.Id &&
                u.UserId == request.UserId);

            if (user != null)
            {
                _context.AccountUsers.Remove(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }
    }
}
