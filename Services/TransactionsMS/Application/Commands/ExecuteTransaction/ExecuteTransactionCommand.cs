using MediatR;

namespace Application.Commands.ExecuteTransactions
{
    public class ExecuteTransactionCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public ExecuteTransactionCommand(int id)
        {
            Id = id;
        }
    }
}
