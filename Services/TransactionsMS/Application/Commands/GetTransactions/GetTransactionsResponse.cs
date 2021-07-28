using Domain;
using System.Collections.Generic;

namespace Application.Commands.GetTransactions
{
    public class GetTransactionsResponse
    {
        public List<Transaction> Transactions { get; set; }
    }
}
