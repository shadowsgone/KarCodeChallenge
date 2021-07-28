using Domain;
using MediatR;

namespace Application.Commands.UpdateAccount
{
    public class UpdateAccountCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public UpdateAccountCommand(int id, Account account)
        {
            Id = id;
            Account = account;
        }
    }
}
