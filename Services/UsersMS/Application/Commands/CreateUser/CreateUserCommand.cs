using Domain;
using MediatR;

namespace Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public User User { get; set; }
        public CreateUserCommand(User user)
        {
            User = user;
        }
    }
}
