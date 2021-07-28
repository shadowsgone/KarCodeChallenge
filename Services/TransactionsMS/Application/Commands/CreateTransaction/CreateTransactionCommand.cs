using Domain;
using MediatR;

namespace Application.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public Transaction Transaction { get; set; }
        public CreateTransactionCommand(Transaction transaction)
        {
            Transaction = transaction;
        }
    }
}
