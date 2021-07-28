namespace Domain
{
    public class Transaction
    {
        public int AccountId { get; set; }
        public TransactionTypes Type { get; set; }
        public decimal Amount { get; set; }
    }
}
