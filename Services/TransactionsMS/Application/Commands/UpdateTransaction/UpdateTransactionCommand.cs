using Domain;
using MediatR;

namespace Application.Commands.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public Transaction Transaction { get; set; }
        public UpdateTransactionCommand(int id, Transaction transaction)
        {
            Id = id;
            Transaction = transaction;
        }
    }
}
