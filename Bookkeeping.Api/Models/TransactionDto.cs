namespace Bookkeeping.Api.Models;

public class TransactionDto
{
    public required DateTime TxnDate { get; set; }
    public required string Merchant { get; set; }
    public required string Category { get; set; }
    public required decimal Amount { get; set; }
    public required string PaymentMethod { get; set; }
}