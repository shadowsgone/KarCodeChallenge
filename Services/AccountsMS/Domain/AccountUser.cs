using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AccountUser
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
    }
}
