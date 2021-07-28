using Domain;
using MediatR;

namespace Application.Commands.GetTransaction
{
    public class GetTransactionCommand : IRequest<Transaction>
    {
        public int Id { get; set; }
        public GetTransactionCommand(int id)
        {
            Id = id;
        }
    }
}
