using Domain;
using MediatR;

namespace Application.Commands.DeleteAccount
{
    public class DeleteAccountCommand : IRequest<BankAccount>
    {
        public int BankId { get; set; }
        public int AccountId { get; set; }
        public DeleteAccountCommand(int bankId, int accountId)
        {
            BankId = bankId;
            AccountId = accountId;
        }
    }
}
