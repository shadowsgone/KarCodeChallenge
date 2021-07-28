using Domain;
using MediatR;

namespace Application.Commands.CreateBank
{
    public class CreateBankCommand : IRequest<int>
    {
        public Bank Bank { get; set; }
        public CreateBankCommand(Bank bank)
        {
            Bank = bank;
        }
    }
}
