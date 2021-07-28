using Domain;
using MediatR;

namespace Application.Commands.GetBank
{
    public class GetBankCommand : IRequest<Bank>
    {
        public int Id { get; set; }
        public GetBankCommand(int id)
        {
            Id = id;
        }
    }
}
