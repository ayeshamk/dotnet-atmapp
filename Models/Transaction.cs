namespace ATMApp.Models;

public class Transaction
{
    public DateTime Timestamp { get; set; }
    public AccountType? From { get; set; }
    public AccountType? To { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } 
}