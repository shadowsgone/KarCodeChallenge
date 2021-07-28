using System;

namespace Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccountTypes Type { get; set; }
        public int BankId { get; set; }
        public int OwnerId { get; set; }
        public decimal Balance { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
