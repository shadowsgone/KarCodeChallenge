using Domain;
using MediatR;

namespace Application.Commands.DeleteAccoutUser
{
    public class DeleteAccountUserCommand : IRequest<AccountUser>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DeleteAccountUserCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
