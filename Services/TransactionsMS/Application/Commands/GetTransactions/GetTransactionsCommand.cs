using MediatR;
using System.Collections.Generic;

namespace Application.Commands.GetTransactions
{
    public class GetTransactionsCommand : IRequest<GetTransactionsResponse>
    {
        public ICollection<int> AccountIds { get; set; }
        public ICollection<int> UserIds { get; set; }
        public bool IsActive { get; set; }
    }
}
