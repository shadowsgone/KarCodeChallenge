using Domain;
using MediatR;

namespace Application.Commands.UpdateBalance
{
    public class UpdateBalanceCommand : IRequest<decimal>
    {
        public int Id { get; set; }
        public Transaction Transaction { get; set; }
        public UpdateBalanceCommand(int id, Transaction transaction)
        {
            Id = id;
            Transaction = transaction;
        }
    }
}
