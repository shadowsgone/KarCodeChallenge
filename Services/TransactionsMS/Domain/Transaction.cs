using System;

namespace Domain
{
    public class Transaction
    {
        //TODO: Should think about using a Guid or BigInt as we will run out of Ids very quickly
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public TransactionTypes Type { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public DateTimeOffset? ExecutionDate { get; set; }
        public bool HasExecuted { get; set; }
        public decimal Amount { get; set; }
        public int? TransferedAccountId { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
