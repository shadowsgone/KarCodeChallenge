using Domain;
using MediatR;

namespace Application.Commands.DeleteAccount
{
    public class DeleteAccountCommand : IRequest<Account>
    {
        public int Id { get; set; }
        public DeleteAccountCommand(int id)
        {
            Id = id;
        }
    }
}
