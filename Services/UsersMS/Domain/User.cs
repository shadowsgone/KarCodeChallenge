using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
