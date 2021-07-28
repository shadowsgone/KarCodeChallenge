using Domain;
using MediatR;

namespace Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public User User { get; set; }
        public UpdateUserCommand(int id, User user)
        {
            Id = id;
            User = user;
        }
    }
}
