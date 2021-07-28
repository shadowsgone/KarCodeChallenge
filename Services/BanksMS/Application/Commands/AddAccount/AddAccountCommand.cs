using MediatR;

namespace Application.Commands.AddAccount
{
    public class AddAccountCommand : IRequest<int>
    {
        public int BankId { get; set; }
        public int AccountId { get; set; }
        public AddAccountCommand(int bankId, int accountId)
        {
            BankId = bankId;
            AccountId = accountId;
        }
    }
}
