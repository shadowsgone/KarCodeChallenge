using Domain;
using MediatR;

namespace Application.Commands.GetUser
{
    public class GetUserCommand : IRequest<User>
    {
        public int Id { get; set; }
        public GetUserCommand(int id)
        {
            Id = id;
        }
    }
}
