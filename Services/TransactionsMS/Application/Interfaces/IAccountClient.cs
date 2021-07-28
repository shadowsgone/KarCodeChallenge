using Domain;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountClient
    {
        Task<bool> UpdateAccountBalanceAsync(Transaction transaction);
    }
}
