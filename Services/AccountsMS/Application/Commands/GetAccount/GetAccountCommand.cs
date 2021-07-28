using Domain;
using MediatR;

namespace Application.Commands.GetAccount
{
    public class GetAccountCommand : IRequest<Account>
    {
        public int Id { get; set; }
        public GetAccountCommand(int id)
        {
            Id = id;
        }
    }
}
