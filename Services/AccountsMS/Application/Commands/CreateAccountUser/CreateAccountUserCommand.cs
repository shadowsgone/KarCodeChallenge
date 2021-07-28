using Domain;
using MediatR;

namespace Application.Commands.CreateAccountUser
{
    public class CreateAccountUserCommand : IRequest<int>
    {
        public int Id { get; set; }
        public AccountUser AccountUser { get; set; }
        public CreateAccountUserCommand(int id, AccountUser accountUser)
        {
            Id = id;
            AccountUser = accountUser;
        }
    }
}
