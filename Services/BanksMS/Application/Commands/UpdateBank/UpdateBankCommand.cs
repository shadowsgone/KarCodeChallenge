using Domain;
using MediatR;

namespace Application.Commands.UpdateBank
{
    public class UpdateBankCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public Bank Bank { get; set; }
        public UpdateBankCommand(int id, Bank bank)
        {
            Id = id;
            Bank = bank;
        }
    }
}
