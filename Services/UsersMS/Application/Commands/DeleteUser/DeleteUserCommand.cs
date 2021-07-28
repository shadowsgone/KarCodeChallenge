using Domain;
using MediatR;

namespace Application.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<User>
    {
        public int Id { get; set; }
        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
