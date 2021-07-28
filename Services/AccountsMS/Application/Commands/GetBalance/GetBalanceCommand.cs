using MediatR;

namespace Application.Commands.GetBalance
{
    public class GetBalanceCommand : IRequest<decimal>
    {
        public int Id { get; set; }
        public GetBalanceCommand(int id)
        {
            Id = id;
        }
    }
}
