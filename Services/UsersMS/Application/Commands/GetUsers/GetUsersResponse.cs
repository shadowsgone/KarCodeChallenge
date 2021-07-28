using Domain;
using System.Collections.Generic;

namespace Application.Commands.GetUsers
{
    public class GetUsersResponse
    {
        public List<User> Users { get; set; }
    }
}
