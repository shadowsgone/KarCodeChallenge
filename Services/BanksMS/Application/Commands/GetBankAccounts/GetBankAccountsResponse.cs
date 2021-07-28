using Domain;
using System.Collections.Generic;

namespace Application.Commands.GetBankAccounts
{
    public class GetBankAccountsResponse
    {
        public List<BankAccount> BankAccounts { get; set; }
    }
}
