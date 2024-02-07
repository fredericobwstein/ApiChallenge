namespace FulltechAPI.Core.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        public int TargetAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
