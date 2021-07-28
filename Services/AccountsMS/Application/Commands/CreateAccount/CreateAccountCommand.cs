using Domain;
using MediatR;

namespace Application.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<int>
    {
        public Account Account { get; set; }
        public CreateAccountCommand(Account account)
        {
            Account = account;
        }
    }
}
