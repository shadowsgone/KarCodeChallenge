using Domain;
using System.Collections.Generic;

namespace Application.Commands.GetAccountUsers
{
    public class GetAccountUsersResponse
    {
        public List<AccountUser> AccountUsers { get; set; }
    }
}
