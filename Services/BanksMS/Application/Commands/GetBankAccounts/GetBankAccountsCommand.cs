using MediatR;

namespace Application.Commands.GetBankAccounts
{
    public class GetBankAccountsCommand : IRequest<GetBankAccountsResponse>
    {
        public int Id { get; set; }
        public GetBankAccountsCommand(int id)
        {
            Id = id;
        }
    }
}
